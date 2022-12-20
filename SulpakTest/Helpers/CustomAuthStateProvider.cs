using Microsoft.AspNetCore.Components.Authorization;
using SulpakTest.Interfaces;
using System.Security.Claims;

namespace SulpakTest.Helpers
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private IAccountService _accountService;

        public CustomAuthStateProvider(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = new AuthenticationState(new ClaimsPrincipal());
            var currentUser = await _accountService.GetCurrentUser();

            if (!string.IsNullOrEmpty(currentUser?.Login))
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, currentUser.Login),
                    new Claim(ClaimTypes.Role, currentUser.Role)
                }, "test authentication type");

                state = new AuthenticationState(new ClaimsPrincipal(identity));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
