using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Security.Pages
{
    public class LoginModel : PageModel
    {
        public async Task OnGet()
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri("/")
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            Data.User.LoggedInUser = User.Identity.Name;
        }
    }
}