namespace PassWarden.Models
{
    /// <summary>
    /// Defines the possible strength grades for a password based on its complexity and security.
    /// </summary>
    public enum PasswordGrade
    {
        /// <summary>
        /// Represents a password that is extremely weak and easily guessable or cracked.
        /// </summary>
        SuperWeak,

        /// <summary>
        /// Represents a weak password that may be vulnerable to common attacks like dictionary or brute force.
        /// </summary>
        Weak,

        /// <summary>
        /// Represents a password of average strength, suitable for general use but could be improved.
        /// </summary>
        Regular,

        /// <summary>
        /// Represents a strong password that is difficult to guess or crack using typical methods.
        /// </summary>
        Strong,

        /// <summary>
        /// Represents a very strong password, offering high security against various attack methods.
        /// </summary>
        SuperStrong
    }

}
