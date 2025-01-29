namespace PassWarden
{
    /// <summary>
    /// Provides methods for hashing passwords and verifying password hashes.
    /// </summary>
    public class HashProvider
    {
        /// <summary>
        /// Hashes the provided password using the bcrypt algorithm and returns the hash as a string.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>The hashed password represented as a string.</returns>
        public static string HashPassword(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        /// <summary>
        /// Verifies if the provided password, when hashed, matches the provided hash.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="hash">The expected hash to compare against.</param>
        /// <returns>True if the hashed password matches the provided hash, otherwise false.</returns>
        public static bool VerifyPassword(string password, string hash) =>
            HashPassword(password) == hash;
    }
}
