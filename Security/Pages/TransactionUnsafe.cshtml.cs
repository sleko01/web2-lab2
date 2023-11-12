using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Security.Pages
{
    public class TransactionUnsafeModel : PageModel
    {
        public string Sender = string.Empty;
        public string Recipient = string.Empty;
        
        public async Task OnGet(string recipient)
        {
            //this does not protect the user from CSRF since I am now bypassing the internal framework check and doing my own which is not secure
            if (Data.User.LoggedInUser != string.Empty)
            {
                Console.WriteLine(Data.User.LoggedInUser);
                Console.WriteLine(recipient);
                Sender = Data.User.LoggedInUser;
                this.Recipient = recipient;
            }
        }
    }
}