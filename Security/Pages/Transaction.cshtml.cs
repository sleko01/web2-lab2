using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Security.Pages
{
    public class TransactionModel : PageModel
    {
        public string? sender;
        public string? recipient;
        
        public async Task OnGet(string recipient)
        {
            //this protects the user from CSRF internally since the User.Identity.Name will be null if CSRF is being attempted
            if (User.Identity.Name is not null)
            {
                Console.WriteLine(User.Identity.Name);
                Console.WriteLine(recipient);
                sender = User.Identity.Name;
                this.recipient = recipient;
            }
        }
    }
}