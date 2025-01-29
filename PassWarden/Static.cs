using PassWarden.Models;
using PassWarden.Resources;

namespace PassWarden
{
    internal static class Static
    {
        private readonly static string _apiUrl = "https://api.pwnedpasswords.com/range/";

        internal static int GetCharSetSize(string password)
        {
            int charSetSize = 0;

            if (password.Any(char.IsLower)) charSetSize += 26;
            if (password.Any(char.IsUpper)) charSetSize += 26;
            if (password.Any(char.IsDigit)) charSetSize += 10;
            if (password.Any(c => !char.IsLetterOrDigit(c))) charSetSize += 32;

            return charSetSize;
        }

        internal static async Task<bool> IsPwned(string password, CancellationToken cancellationToken = default)
        {
            string hash = HashProvider.HashPassword(password);
            string prefix = hash[..5];
            string suffix = hash[5..];

            using var client = new HttpClient();
            var response = await client.GetAsync(_apiUrl + prefix, cancellationToken);
            string result = await response.Content.ReadAsStringAsync(cancellationToken);

            return result.Split('\n').Any(line => line.StartsWith(suffix, StringComparison.OrdinalIgnoreCase));
        }

        internal static double GetEntropy(string password)
        {
            if (string.IsNullOrEmpty(password))
                return 0;

            var charSetSize = GetCharSetSize(password);

            return Math.Log(charSetSize, 2) * password.Length;
        }

        internal static PasswordStrengthResult GetStrength(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException(string.Format(Error.NullOrEmpty, nameof(password)));

            double entropy = GetEntropy(password);
            int score;

            if (entropy < 28) score = 1;
            else if (entropy < 36) score = 2;
            else if (entropy < 60) score = 3;
            else if (entropy < 128) score = 4;
            else score = 5;

            var grade = score switch
            {
                1 => PasswordGrade.SuperWeak,
                2 => PasswordGrade.Weak,
                3 => PasswordGrade.Regular,
                4 => PasswordGrade.Strong,
                _ => PasswordGrade.SuperStrong,
            };

            return new PasswordStrengthResult
            {
                Score = score,
                Entropy = entropy,
                Grade = grade
            };
        }
    }
}
