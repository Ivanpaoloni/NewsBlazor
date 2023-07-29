using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using NewsBlazor.Helpers;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.Json;

namespace NewsBlazor.Auth
{
    public interface ILoginService
    {
        Task Login(string token);
        Task Logout();
    }

    public class JWTAuthenticationProvider : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;
        private readonly ILoginService loginService;
        public static readonly string TOKENKEY = "TOKENKEY";
        private AuthenticationState Anonimus = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        
        public JWTAuthenticationProvider(IJSRuntime js, HttpClient httpClient, ILoginService loginService) 
        {
            this.js = js;
            this.httpClient = httpClient;
            this.loginService = loginService;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return Anonimus;
            }
            
            return CreateAuthenticationState(token);
            
        }

        public async Task Login(string token)
        {
            await js.SetInLocalStorage(TOKENKEY, token);
            var authState = CreateAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
            await js.RemoveItem(TOKENKEY);
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimus));
        }

        //funcion para crear la cabecera por defecto con el token
        private AuthenticationState CreateAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }


        //codigo hecho por el creador de blazor para parsear el jwt

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
