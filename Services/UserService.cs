using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewsBlazor.Auth;
using NewsBlazor.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace NewsBlazor.Services
{
    public interface IUserService
    {
        Task<AuthorizationResponse> Create(UserCredentials userCredentials);
        Task<AuthorizationResponse> LoginUser(UserCredentials userCredentials);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient )
        {
            this.httpClient = httpClient;
        }
        public async Task<AuthorizationResponse>Create(UserCredentials userCredentials)
        {
            try
            {
                var apiUrl = "https://localhost:7081/api/account/register";
                var result = await httpClient.PostAsJsonAsync(apiUrl, userCredentials);
                if (result.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta como una cadena JSON
                    var responseContent = await result.Content.ReadAsStringAsync();

                    // Deserializa la cadena JSON en un objeto AuthorizationResponse
                    var authorizationResponse = JsonSerializer.Deserialize<AuthorizationResponse>(responseContent);
                    //await loginService.Login(authorizationResponse.token);
                    // Usa el token como necesites
                    return authorizationResponse;
                }
                return new AuthorizationResponse();
            }
            catch (Exception ex)
            {
                return new AuthorizationResponse();
            }

        }
        public async Task<AuthorizationResponse> LoginUser(UserCredentials userCredentials)
        {
            
            try
            {
                var apiUrl = "https://localhost:7081/api/account/login";
                var result = await httpClient.PostAsJsonAsync(apiUrl, userCredentials);
                if (result.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta como una cadena JSON
                    var responseContent = await result.Content.ReadAsStringAsync();

                    // Deserializa la cadena JSON en un objeto AuthorizationResponse
                    var authorizationResponse = JsonSerializer.Deserialize<AuthorizationResponse>(responseContent);
                    //await loginService.Login(authorizationResponse.token);
                    // Usa el token como necesites
                    return authorizationResponse;
                }
                return new AuthorizationResponse();
            }
            catch (Exception ex)
            {
                return new AuthorizationResponse();
            }
        }
    }
}
