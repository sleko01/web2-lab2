using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Security.Shared
{
    public partial class MainLayout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private bool IsLoggedIn { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (authenticationState.User.Identity.Name is null)
            {
                IsLoggedIn = false;
                return;
            }
            IsLoggedIn = authenticationState.User.Identity.IsAuthenticated;
        }

        public void RedirectToLogin()
        {
            NavigationManager.NavigateTo("/login");
        }
    }
}