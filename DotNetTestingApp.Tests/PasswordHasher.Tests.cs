namespace Services.PasswordHasher
{
    using Xunit;

    public class PasswordHasherServiceTests
    {
        private readonly PasswordHasherService _passwordHasherService;

        public PasswordHasherServiceTests()
        {
            _passwordHasherService = new PasswordHasherService();
        }

        [Fact]
        public void HashPassword_ShouldGenerateUniqueHashesForDifferentInputs()
        {
            string password1 = "Password123!";
            string password2 = "Password456!";

            var hash1 = _passwordHasherService.HashPassword(password1);
            var hash2 = _passwordHasherService.HashPassword(password2);

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void HashPassword_ShouldThrowArgumentException_WhenPasswordIsNull()
        {
            Assert.Throws<ArgumentException>(() => _passwordHasherService.HashPassword(null));
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_ForValidPassword()
        {
            string password = "TestPassword123!";
            var hashedPassword = _passwordHasherService.HashPassword(password);

            var result = _passwordHasherService.VerifyPassword(hashedPassword, password);

            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_ForInvalidPassword()
        {
            string password = "TestPassword123!";
            string invalidPassword = "WrongPassword!";
            var hashedPassword = _passwordHasherService.HashPassword(password);

            var result = _passwordHasherService.VerifyPassword(hashedPassword, invalidPassword);

            Assert.False(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_ForMalformedHashedPassword()
        {
            string password = "TestPassword123!";
            var result = _passwordHasherService.VerifyPassword("InvalidHashFormat", password);

            Assert.False(result);
        }
    }
}
