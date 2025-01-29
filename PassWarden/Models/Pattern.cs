namespace PassWarden.Models
{
    /// <summary>
    /// Defines the possible patterns that can be detected in a password.
    /// </summary>
    public enum Pattern
    {
        /// <summary>
        /// A pattern where characters in the password are repeated (e.g., "aaabbb").
        /// </summary>
        RepeatingCharacters,

        /// <summary>
        /// A pattern where characters in the password form a recognizable keyboard sequence (e.g., "qwerty").
        /// </summary>
        KeyboardSequence,

        /// <summary>
        /// A pattern where the password contains sequential numbers (e.g., "1234" or "9876").
        /// </summary>
        SequentialNumbers,

        /// <summary>
        /// A pattern where the password follows a date format (e.g., "MMDDYYYY" or "DD-MM-YYYY").
        /// </summary>
        DateFormat,

        /// <summary>
        /// A pattern where characters alternate in a predictable sequence (e.g., "ababab").
        /// </summary>
        AlternatingPattern,

        /// <summary>
        /// A pattern indicating low diversity in the characters used in the password (e.g., repeated use of the same character).
        /// </summary>
        LowDiversity,

        /// <summary>
        /// A pattern where common words or phrases are used in the password (e.g., "password123" or "qwerty").
        /// </summary>
        CommonWords,

        /// <summary>
        /// A pattern where the password contains repeated patterns (e.g., "abcabc").
        /// </summary>
        RepeatedPattern,

        /// <summary>
        /// A pattern where the password contains sequential letters (e.g., "abc" or "xyz").
        /// </summary>
        SequentialLetters,

        /// <summary>
        /// A pattern where the password contains repeated keyboard patterns (e.g., "qwertyqwerty").
        /// </summary>
        RepeatedKeyboardPatterns,

        /// <summary>
        /// No identifiable pattern in the password.
        /// </summary>
        None
    }

}
