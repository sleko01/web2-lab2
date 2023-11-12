using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Npgsql;
using Security.Data;

namespace Security.Pages
{
    public partial class Index
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        public string Recipient { get; set; } = "";
        private Query Model { get; set; } = new();
        private List<User> Users { get; set; } = new();
        private async void FindUser()
        {
            Users.Clear();
            const string connectionString = "Host=dpg-cl723e2uuipc73f5v6ig-a.frankfurt-postgres.render.com;Username=securitydb_user;Password=D58OE69inTcFCZyATrGkjkfR7pGx9uoM;Database=securitydb;";
            await using var dataSource = NpgsqlDataSource.Create(connectionString);
            if (Model.UseSqlInjection)
            {
                string userInput = "'0' OR 1=1";
                await using var command = dataSource.CreateCommand($"SELECT * FROM \"userinfo\" WHERE id = {Model.UserId};");
                await using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Users.Add(new User
                    {
                        Id = reader[0].ToString(),
                        FullName = reader[1].ToString(),
                        Username = reader[2].ToString(),
                        Password = reader[3].ToString()
                    });
                    StateHasChanged();
                }
            }
            else
            {
                int userId;
                try
                {
                    userId = int.Parse(Model.UserId);
                } catch(FormatException)
                {
                    Console.WriteLine("Invalid user id");
                    return;
                }
                await using var command = dataSource.CreateCommand($"SELECT * FROM \"userinfo\" WHERE id = {Model.UserId} LIMIT 1;");
                await using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Users.Add(new User
                    {
                        Id = reader[0].ToString(),
                        FullName = reader[1].ToString(),
                        Username = reader[2].ToString(),
                        Password = reader[3].ToString()
                    });
                    if (Users.Count > 1)
                    {
                        // This should never happen
                        Users.Clear();
                        throw new Exception("Someone might be trying to SQL inject us!");
                    }
                    StateHasChanged();
                }
            }
        }

        public async void RedirectToTransaction()
        {
            await JSRuntime.InvokeVoidAsync("redirectToTransaction", Recipient);
        }
    }
}