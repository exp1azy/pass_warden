namespace PassWarden.Models
{
    /// <summary>
    /// Represents the rules for validating a password, including length constraints and character requirements.
    /// </summary>
    public class PasswordValidationRules
    {
        /// <summary>
        /// Gets or sets the minimum length of the password.
        /// The password must have at least this many characters to be considered valid.
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the password.
        /// The password must not exceed this many characters to be considered valid.
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the password must contain at least one lowercase letter.
        /// </summary>
        public bool RequireLowercase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the password must contain at least one uppercase letter.
        /// </summary>
        public bool RequireUppercase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the password must contain at least one digit.
        /// </summary>
        public bool RequireDigit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the password must contain at least one special character (non-letter, non-digit).
        /// </summary>
        public bool RequireSpecialCharacters { get; set; }
    }

}
