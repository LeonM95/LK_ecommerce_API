using BCrypt.Net;

namespace src.Utils
{
    public static class PasswordHasher
    {
        // to create a new hash from a plain-text password
        public static string Hash(string password)
        {
            // The 'using' statement lets you use the short version
            return BCrypt.HashPassword(password);
        }

        // to verify a plain-text password against a stored hash
        public static bool Verify(string password, string hash)
        {
            return BCrypt.Verify(password, hash);
        }
    }
}