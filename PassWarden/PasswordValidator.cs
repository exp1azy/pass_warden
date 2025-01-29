using PassWarden.Models;
using PassWarden.Resources;
using System.Text.RegularExpressions;

namespace PassWarden
{
    /// <summary>
    /// Provides methods to validate passwords against custom rules, patterns, and stop lists.
    /// </summary>
    public class PasswordValidator
    {
        /// <summary>
        /// Validates a password against a regular expression pattern.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <param name="regexPattern">The regular expression pattern to validate against.</param>
        /// <returns>True if the password matches the pattern; otherwise, false.</returns>
        public static bool ValidatePassword(string password, string regexPattern) =>
            Regex.IsMatch(password, regexPattern);

        /// <summary>
        /// Validates a password by ensuring it does not contain any items from a stop list.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <param name="stopList">A collection of strings that are not allowed in the password.</param>
        /// <returns>True if the password does not contain any items from the stop list; otherwise, false.</returns>
        public static bool ValidateByStopList(string password, IEnumerable<string> stopList) =>
            !stopList.Any(password.Contains);

        /// <summary>
        /// Validates a password based on a set of rules.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <param name="rules">An instance of <see cref="PasswordValidationRules"/> defining the validation criteria.</param>
        /// <returns>True if the password meets all the specified rules; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown if the minimum length is greater than or equal to the maximum length in the rules.</exception>
        public static bool ValidatePassword(string password, PasswordValidationRules rules)
        {
            if (rules.MinLength >= rules.MaxLength)
                throw new ArgumentException(string.Format(Error.RangeError, nameof(rules.MinLength), nameof(rules.MaxLength)));

            if (rules.MinLength > password.Length || rules.MaxLength < password.Length)
                return false;
            if (rules.RequireLowercase && !password.Any(char.IsLower))
                return false;
            if (rules.RequireUppercase && !password.Any(char.IsUpper))
                return false;
            if (rules.RequireDigit && !password.Any(char.IsDigit))
                return false;
            if (rules.RequireSpecialCharacters && !password.Any(c => !char.IsLetterOrDigit(c)))
                return false;

            return true;
        }
    }
}
