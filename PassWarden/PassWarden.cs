using Microsoft.Extensions.Configuration;
using PassWarden.Models;
using PassWarden.Resources;
using System.Numerics;

namespace PassWarden
{
    /// <summary>
    /// Provides functionality for password analysis, generation, validation, and hash computation.
    /// Also includes methods for calculating brute-force cracking times based on various parameters.
    /// </summary>
    public class PassWarden
    {
        private readonly PasswordAnalyzer _passAnalyzer = new();
        private readonly PasswordGenerator _passGenerator = new();
        private readonly PasswordValidator _passValidator = new();
        private readonly HashProvider _hashProvider = new();
        private readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile("crackingspeed.json").Build();

        /// <summary>
        /// Gets an instance of <see cref="PasswordAnalyzer"/> to analyze password strength.
        /// </summary>
        public PasswordAnalyzer Analyzer => _passAnalyzer;

        /// <summary>
        /// Gets an instance of <see cref="PasswordGenerator"/> to generate secure passwords.
        /// </summary>
        public PasswordGenerator Generator => _passGenerator;

        /// <summary>
        /// Gets an instance of <see cref="PasswordValidator"/> to validate password compliance with defined rules.
        /// </summary>
        public PasswordValidator Validator => _passValidator;

        /// <summary>
        /// Gets an instance of <see cref="HashProvider"/> to compute and verify password hashes.
        /// </summary>
        public HashProvider HashProvider => _hashProvider;

        /// <summary>
        /// Calculates the time required to brute-force the given password using a specified hashing algorithm.
        /// </summary>
        /// <param name="password">The password to evaluate.</param>
        /// <param name="algorithm">The hashing algorithm used in the brute-force scenario. Default is SHA1.</param>
        /// <param name="timeFormat">The desired time format for the result. Default is days.</param>
        /// <returns>The estimated brute-force time in the specified format.</returns>
        /// <exception cref="ArgumentException">Thrown when the password is null, empty, or whitespace.</exception>
        public double CalculateBruteForceTime(string password, HashAlgorithm algorithm = HashAlgorithm.SHA1, TimeFormat timeFormat = TimeFormat.Days) =>
            GetBruteForceTime(password, double.Parse(_config[algorithm.ToString()]!), timeFormat);

        /// <summary>
        /// Calculates the time required to brute-force the given password based on the number of attempts per second.
        /// </summary>
        /// <param name="password">The password to evaluate.</param>
        /// <param name="attemptsPerSecond">The number of password guesses performed per second.</param>
        /// <param name="timeFormat">The desired time format for the result. Default is days.</param>
        /// <returns>The estimated brute-force time in the specified format.</returns>
        /// <exception cref="ArgumentException">Thrown when the password is null, empty, or whitespace, or if attemptsPerSecond is less than or equal to zero.</exception>
        public double CalculateBruteForceTime(string password, double attemptsPerSecond, TimeFormat timeFormat = TimeFormat.Days) =>
            GetBruteForceTime(password, attemptsPerSecond, timeFormat);

        private static double GetBruteForceTime(string password, double attemptsPerSecond, TimeFormat timeFormat)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(string.Format(Error.NullOrEmpty, nameof(password)));

            if (attemptsPerSecond <= 0)
                throw new ArgumentException(string.Format(Error.LessOrEqualToZero, nameof(attemptsPerSecond)));

            int charSetSize = Static.GetCharSetSize(password);
            int passwordLength = password.Length;
            var totalCombinations = BigInteger.Pow(charSetSize, passwordLength);

            double bruteForceTime = (double)totalCombinations / attemptsPerSecond;

            return timeFormat switch
            {
                TimeFormat.Years => bruteForceTime / 31557600,
                TimeFormat.Months => bruteForceTime / 2629743,
                TimeFormat.Weeks => bruteForceTime / 604800,
                TimeFormat.Days => bruteForceTime / 86400,
                TimeFormat.Hours => bruteForceTime / 3600,
                TimeFormat.Minutes => bruteForceTime / 60,
                TimeFormat.Seconds => bruteForceTime,
                _ => throw new Exception("Invalid TimeFormat specified."),
            };
        }
    }
}
