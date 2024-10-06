namespace DotnetTestingApp.Api.Repositories.Users
{
    internal sealed class RegisterUser
    {
        public sealed record request(string Email, string FirstName, string LastName, string Password);
    }
}
