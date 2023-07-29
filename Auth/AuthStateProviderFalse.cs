using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace NewsBlazor.Auth
{
    public class AuthStateProviderFalse : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
    }
}
