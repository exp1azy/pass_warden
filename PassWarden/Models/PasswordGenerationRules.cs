namespace PassWarden.Models
{
    /// <summary>
    /// Represents the rules for generating a password, including the required number of characters in different categories.
    /// </summary>
    public class PasswordGenerationRules
    {
        /// <summary>
        /// Gets or sets the number of lowercase letters required in the generated password.
        /// </summary>
        public int NumberOfLowercase { get; set; }

        /// <summary>
        /// Gets or sets the number of uppercase letters required in the generated password.
        /// </summary>
        public int NumberOfUppercase { get; set; }

        /// <summary>
        /// Gets or sets the number of digits required in the generated password.
        /// </summary>
        public int NumberOfDigits { get; set; }

        /// <summary>
        /// Gets or sets the number of special characters required in the generated password.
        /// Special characters include symbols such as !, @, #, etc.
        /// </summary>
        public int NumberOfSpecialCharacters { get; set; }
    }

}
