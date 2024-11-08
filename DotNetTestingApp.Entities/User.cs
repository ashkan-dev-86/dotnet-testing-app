namespace DotnetTestingApp.Entities
{
    public class User
    {
        public Ulid Id { get; set; } = new Ulid();
        public string? FirstName { get; set; }
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
    }
}
