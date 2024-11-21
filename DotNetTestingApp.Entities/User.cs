namespace DotnetTestingApp.Entities
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();
        public string? FirstName { get; set; }
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
    }
}
