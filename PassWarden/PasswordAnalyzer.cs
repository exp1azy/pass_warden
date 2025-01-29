using PassWarden.Models;
using PassWarden.Resources;
using System.Text.RegularExpressions;

namespace PassWarden
{
    /// <summary>
    /// Provides methods for analyzing password properties, strength, patterns, and similarity.
    /// </summary>
    public partial class PasswordAnalyzer
    {
        /// <summary>
        /// Asynchronously checks if the specified password has been compromised in a known data breach.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation, containing a boolean indicating whether the password is compromised.</returns>
        public async Task<bool> IsPwned(string password, CancellationToken cancellationToken = default) =>
            await Static.IsPwned(password, cancellationToken);

        /// <summary>
        /// Calculates the entropy of the specified password, which represents its randomness and unpredictability.
        /// </summary>
        /// <param name="password">The password to analyze.</param>
        /// <returns>A double value representing the entropy of the password.</returns>
        public double GetEntropy(string password) =>
            Static.GetEntropy(password);

        /// <summary>
        /// Evaluates the strength of the specified password based on its complexity, length, and other factors.
        /// </summary>
        /// <param name="password">The password to evaluate.</param>
        /// <returns>A <see cref="PasswordStrengthResult"/> object containing the evaluation details.</returns>
        public PasswordStrengthResult GetStrength(string password) =>
            Static.GetStrength(password);

        /// <summary>
        /// Analyzes the frequency of each character in the specified password.
        /// </summary>
        /// <param name="password">The password to analyze.</param>
        /// <returns>A dictionary where keys are characters and values are their respective frequencies in the password.</returns>
        /// <exception cref="ArgumentException">Thrown when the password is null or empty.</exception>
        public Dictionary<char, int> GetSymbolFrequency(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException(string.Format(Error.NullOrEmpty, nameof(password)));

            var result = new Dictionary<char, int>();

            for (int i = 0; i < password.Length; i++)
            {
                char symbol = password[i];
                int frequency = 0;

                for (int j = 0; j < password.Length; j++)                
                    if (password[j] == symbol) frequency++;
                
                if (!result.TryAdd(symbol, frequency)) result[symbol] = frequency;
            }

            return result;
        }

        /// <summary>
        /// Detects patterns in the specified password, such as repeating characters, sequential letters, or numbers.
        /// </summary>
        /// <param name="password">The password to analyze for patterns.</param>
        /// <returns>A list of detected patterns, or <see cref="Pattern.None"/> if no patterns are found.</returns>
        /// <exception cref="ArgumentException">Thrown when the password is null, empty, or consists only of whitespace.</exception>
        public List<Pattern> DetectPatterns(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(string.Format(Error.NullOrEmpty, nameof(password)));

            var detectedPatterns = new List<Pattern>();

            if (RepeatingCharactersRegex().IsMatch(password))
                detectedPatterns.Add(Pattern.RepeatingCharacters);

            if (SequentialNumbersRegex().IsMatch(password))
            {
                var numbers = password.Where(c => char.GetNumericValue(c) != -1.0).ToArray();
                if (IsSequential(numbers))
                    detectedPatterns.Add(Pattern.SequentialNumbers);
            }

            if (DateFormatRegex().IsMatch(password))
                detectedPatterns.Add(Pattern.DateFormat);

            if (AlternatingPatternRegex().IsMatch(password))
                detectedPatterns.Add(Pattern.AlternatingPattern);

            if (password.Distinct().Count() < password.Length / 2)
                detectedPatterns.Add(Pattern.LowDiversity);

            if (RepeatedPatternRegex().IsMatch(password))
                detectedPatterns.Add(Pattern.RepeatedPattern);

            if (SequentialLettersRegex().IsMatch(password))
            {
                var letters = password.Where(c => char.GetNumericValue(c) == -1.0).ToArray();
                if (IsSequential(letters))
                    detectedPatterns.Add(Pattern.SequentialLetters);
            }

            if (detectedPatterns.Count == 0)
                detectedPatterns.Add(Pattern.None);

            return detectedPatterns;
        }

        /// <summary>
        /// Calculates the similarity between two passwords by comparing their characters at the same positions.
        /// </summary>
        /// <param name="password1">The first password to compare.</param>
        /// <param name="password2">The second password to compare.</param>
        /// <returns>A double value between 0 and 1, representing the similarity percentage of the passwords.</returns>
        public double GetPasswordSimilarity(string password1, string password2)
        {
            int repeatsCount = 0;
            int minLength = Math.Min(password1.Length, password2.Length);
            int maxLength = Math.Max(password1.Length, password2.Length);

            for (int i = 0; i < minLength; i++)
            {
                if (password1[i] == password2[i])
                    repeatsCount++;
            }

            return (double)repeatsCount / maxLength;
        }

        private static bool IsSequential(char[] chars)
        {
            if (chars.Length < 3) return false;

            bool ascending = true;
            bool descending = true;

            for (int i = 1; i < chars.Length; i++)
            {
                if (!char.IsLetterOrDigit(chars[i - 1]) || !char.IsLetterOrDigit(chars[i]))
                    return false;

                int diff = chars[i] - chars[i - 1];

                if (diff == 1)
                    descending = false;
                else if (diff == -1)
                    ascending = false;
                else
                    return false;

                if (!ascending && !descending)
                    return false;
            }

            return ascending || descending;
        }

        [GeneratedRegex(@"(.)\1{2,}")]
        private static partial Regex RepeatingCharactersRegex();

        [GeneratedRegex(@"\d{3,}")]
        private static partial Regex SequentialNumbersRegex();

        [GeneratedRegex(@"(?<!\d)((1\d{3}|20[0-9]{2})[-./\\]?(0[1-9]|1[0-2])[-./\\]?(0[1-9]|[12][0-9]|3[01])|"
               + @"(0[1-9]|[12][0-9]|3[01])[-./\\]?(0[1-9]|1[0-2])[-./\\]?(1\d{3}|20[0-9]{2}))(?!\d)")]
        private static partial Regex DateFormatRegex();

        [GeneratedRegex(@"(.{1,2})\1{2,}")]
        private static partial Regex AlternatingPatternRegex();

        [GeneratedRegex(@"^(..+)\1+$")]
        private static partial Regex RepeatedPatternRegex();

        [GeneratedRegex(@"[a-z]{3,}", RegexOptions.IgnoreCase, "ru-RU")]
        private static partial Regex SequentialLettersRegex();
    }
}
