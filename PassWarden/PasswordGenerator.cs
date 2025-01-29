using PassWarden.Extensions;
using PassWarden.Models;
using PassWarden.Resources;
using System.Text;
using System.Text.Json;

namespace PassWarden
{
    /// <summary>
    /// Provides functionality for generating passwords with various rules and formats, including random passwords, 
    /// mnemonic-based passwords, and passwords derived from phrases.
    /// </summary>
    public class PasswordGenerator
    {
        private readonly Random _random = new();

        private readonly string[] _numbers = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
        private readonly string[] _lowercaseLetters = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];
        private readonly string[] _uppercaseLetters = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"];
        private readonly string[] _specialCharacters = ["!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+", "[", "]", "{", "}", ";", ":", "'", "\"", "\\", "|", ",", "<", ".", ">", "/", "?"];
        private readonly Dictionary<char, string[]> _substitutions = new()
        {
            { 'a', new[] { "@", "4", "A" } },
            { 'b', new[] { "8", "B" } },
            { 'c', new[] { "(", "{", "C" } },
            { 'd', new[] { "D", "|)", "cl" } },
            { 'e', new[] { "3", "E" } },
            { 'f', new[] { "F", "|=" } },
            { 'g', new[] { "9", "G", "&" } },
            { 'h', new[] { "H", "#", "|-|" } },
            { 'i', new[] { "1", "!", "I" } },
            { 'j', new[] { "J", "_|" } },
            { 'k', new[] { "K", "|<" } },
            { 'l', new[] { "1", "|", "L" } },
            { 'm', new[] { "M", "|\\/|" } },
            { 'n', new[] { "N", "|\\|" } },
            { 'o', new[] { "0", "O", "()" } },
            { 'p', new[] { "P", "|*" } },
            { 'q', new[] { "Q", "9" } },
            { 'r', new[] { "R", "|2" } },
            { 's', new[] { "5", "$", "S" } },
            { 't', new[] { "7", "+", "T" } },
            { 'u', new[] { "U", "|_|" } },
            { 'v', new[] { "V", "\\/" } },
            { 'w', new[] { "W", "\\/\\/", "VV" } },
            { 'x', new[] { "X", "><" } },
            { 'y', new[] { "Y", "`/" } },
            { 'z', new[] { "2", "Z" } }
        };

        private MnemonicData? _data;

        /// <summary>
        /// Asynchronously generates a random password using default settings.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, containing the generated password.</returns>
        public async Task<string> GenerateRandomAsync(CancellationToken cancellationToken = default) =>
            await GetRandomAsync(cancellationToken);

        /// <summary>
        /// Synchronously generates a random password using default settings.
        /// </summary>
        /// <returns>The generated password.</returns>
        public string GenerateRandom() =>
            GetRandomAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Generates a password based on the provided rules.
        /// </summary>
        /// <param name="rules">The rules defining the composition of the password, such as the number of uppercase letters, digits, etc.</param>
        /// <returns>The generated password adhering to the specified rules.</returns>
        /// <exception cref="ArgumentException">Thrown when any rule parameter is less than zero.</exception>
        public string Generate(PasswordGenerationRules rules)
        {
            if (rules.NumberOfLowercase < 0)
                throw new ArgumentException(string.Format(Error.LessThanZero, nameof(rules.NumberOfLowercase)));
            if (rules.NumberOfUppercase < 0)
                throw new ArgumentException(string.Format(Error.LessThanZero, nameof(rules.NumberOfUppercase)));
            if (rules.NumberOfDigits < 0)
                throw new ArgumentException(string.Format(Error.LessThanZero, nameof(rules.NumberOfDigits)));
            if (rules.NumberOfSpecialCharacters < 0)
                throw new ArgumentException(string.Format(Error.LessThanZero, nameof(rules.NumberOfSpecialCharacters)));

            var length = rules.NumberOfLowercase + rules.NumberOfUppercase + rules.NumberOfDigits + rules.NumberOfSpecialCharacters;
            var stringBuilder = new StringBuilder();

            while (stringBuilder.ToString().Length < length)
            {
                Append(stringBuilder, 
                    () => stringBuilder.ToString().Where(char.IsLower).Count() < rules.NumberOfLowercase, 
                    _lowercaseLetters[_random.Next(0, _lowercaseLetters.Length)]
                );

                Append(stringBuilder,
                    () => stringBuilder.ToString().Where(char.IsUpper).Count() < rules.NumberOfUppercase,
                    _uppercaseLetters[_random.Next(0, _uppercaseLetters.Length)]
                );

                Append(stringBuilder,
                    () => stringBuilder.ToString().Where(char.IsDigit).Count() < rules.NumberOfDigits,
                    _numbers[_random.Next(0, _numbers.Length)]
                );

                Append(stringBuilder,
                    () => stringBuilder.ToString().Where(c => !char.IsLetterOrDigit(c)).Count() < rules.NumberOfSpecialCharacters,
                    _specialCharacters[_random.Next(0, _specialCharacters.Length)]
                );
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generates a mnemonic-based password, which combines a noun, adjective, verb, special character, and digit.
        /// </summary>
        /// <param name="requiredCase">The casing style for the password (e.g., camelCase, snake_case).</param>
        /// <returns>The generated mnemonic-based password.</returns>
        public string GenerateMnemonic(Case requiredCase)
        {
            string noun = GetRandomFromJson("Nouns");
            string adjective = GetRandomFromJson("Adjectives");
            string verb = GetRandomFromJson("Verbs");
            string specialChar = _specialCharacters[_random.Next(0, _specialCharacters.Length)];
            string digit = _numbers[_random.Next(0, _numbers.Length)];

            string password = string.Join(" ", adjective, noun, verb, specialChar + digit);

            return requiredCase switch
            {
                Case.Camel => password.GetAsCamel(),
                Case.Pascal => password.GetAsPascal(),
                Case.Snake => password.GetAsSnake(),
                Case.ScreamingSnake => password.GetAsScreamingSnake(),
                Case.Kebab => password.GetAsKebabOrTrain(),
                Case.Train => password.GetAsKebabOrTrain(),
                Case.Dot => password.GetAsDot(),
                Case.Upper => password.GetAsUpper(),
                Case.Lower => password.GetAsLower(),
                _ => password
            };
        }

        /// <summary>
        /// Generates a password derived from the specified phrase, applying character substitutions.
        /// </summary>
        /// <param name="phrase">The phrase used to generate the password. Must not contain digits or special characters.</param>
        /// <returns>The generated password based on the phrase.</returns>
        /// <exception cref="ArgumentException">Thrown when the phrase is null, empty, or contains invalid characters.</exception>
        public string GenerateFromPhrase(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
                throw new ArgumentException(string.Format(Error.NullOrEmpty, nameof(phrase)));

            if (phrase.Any(char.IsDigit) || phrase.Any(c => !char.IsLetterOrDigit(c)))
                throw new ArgumentException(string.Format(Error.ContainsDigitsOrCharacters, nameof(phrase)));

            string fused = phrase.Replace(" ", string.Empty).ToLower();
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < fused.Length; i++)
            {
                var letter = _substitutions[fused[i]][_random.Next(0, _substitutions[fused[i]].Length)];
                stringBuilder.Append(letter);
            }

            return stringBuilder.ToString();
        }

        private void Append(StringBuilder stringBuilder, Func<bool> condition, string value)
        {
            if (condition())
            {
                var needToAdd = _random.Next(0, 2);
                if (needToAdd == 0)
                    stringBuilder.Append(value);
            }
        }

        private async Task<string> GetRandomAsync(CancellationToken cancellationToken = default)
        {
            string password = string.Empty;
            bool reliable = false;

            while (!reliable)
            {
                password = GetRandomSymbols(20);
                reliable = await Static.IsPwned(password, cancellationToken) == false && 
                                 Static.GetStrength(password).Score == 5;
            }

            return password;
        }

        private string GetRandomSymbols(int length)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = _random.Next(0, 4);

                if (index == 0)
                    stringBuilder.Append(_numbers[_random.Next(0, _numbers.Length)]);
                else if (index == 1)
                    stringBuilder.Append(_lowercaseLetters[_random.Next(0, _lowercaseLetters.Length)]);
                else if (index == 2)
                    stringBuilder.Append(_uppercaseLetters[_random.Next(0, _uppercaseLetters.Length)]);
                else
                    stringBuilder.Append(_specialCharacters[_random.Next(0, _specialCharacters.Length)]);
            }

            return stringBuilder.ToString();
        }

        private string GetRandomFromJson(string section)
        {
            string json = File.ReadAllText("mnemonicdata.json");
            _data ??= JsonSerializer.Deserialize<MnemonicData>(json)!;

            var listOfMnemonicWords = typeof(MnemonicData).GetProperty(section)!.GetValue(_data) as string[];
            var index = _random.Next(0, listOfMnemonicWords!.Length);

            return listOfMnemonicWords.Skip(index - 1).First();
        }
    }
}
