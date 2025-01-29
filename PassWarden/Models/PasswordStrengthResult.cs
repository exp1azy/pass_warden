namespace PassWarden.Models
{
    /// <summary>
    /// Represents the result of a password strength analysis, including the score, entropy, and grade.
    /// </summary>
    public class PasswordStrengthResult
    {
        /// <summary>
        /// Gets or sets the numeric score that reflects the password's strength. 
        /// Higher values indicate stronger passwords.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the entropy value of the password, which measures its randomness and unpredictability.
        /// A higher value indicates greater password complexity.
        /// </summary>
        public double Entropy { get; set; }

        /// <summary>
        /// Gets or sets the password grade based on its strength, which can be one of the predefined grades in the <see cref="PasswordGrade"/> enum.
        /// </summary>
        public PasswordGrade Grade { get; set; }
    }

}
