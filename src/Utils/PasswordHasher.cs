namespace src.Utils
{
    public static class PasswordHasher
    {
        // to create a new hash from a plain-text password
        public static string Hash(string password)
        {
            // Using the full name to be explicit
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // to verify a plain-text password against a stored hash
        public static bool Verify(string password, string hash)
        {
            // Using the full name to be explicit
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}