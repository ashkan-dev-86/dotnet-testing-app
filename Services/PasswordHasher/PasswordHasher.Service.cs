namespace Services.PasswordHasher
{
    using System.Security.Cryptography;
    using System.Text;

    public class PasswordHasherService : IPasswordHasherService
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            using var hmac = new HMACSHA256();
            var salt = hmac.Key; // Generate salt using the HMAC key.
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(password))
                return false;

            var parts = hashedPassword.Split('.');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = Convert.FromBase64String(parts[1]);

            using var hmac = new HMACSHA256(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(storedHash);
        }
    }
}
