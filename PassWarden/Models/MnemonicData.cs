namespace PassWarden.Models
{
    /// <summary>
    /// Represents a collection of word categories used for generating mnemonic phrases.
    /// </summary>
    public class MnemonicData
    {
        /// <summary>
        /// Gets or sets an array of nouns that can be used in mnemonic phrase generation.
        /// </summary>
        public string[] Nouns { get; set; }

        /// <summary>
        /// Gets or sets an array of adjectives that can describe nouns in mnemonic phrases.
        /// </summary>
        public string[] Adjectives { get; set; }

        /// <summary>
        /// Gets or sets an array of verbs that can represent actions in mnemonic phrases.
        /// </summary>
        public string[] Verbs { get; set; }
    }

}
