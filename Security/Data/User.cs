namespace Security.Data;

public class User
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public static string LoggedInUser { get; set; } = string.Empty;
}