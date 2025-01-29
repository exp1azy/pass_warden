namespace PassWarden.Models
{
    /// <summary>
    /// Represents various cryptographic hash algorithms used for securing and verifying data integrity.
    /// </summary>
    public enum HashAlgorithm
    {
        /// <summary>
        /// Represents the MD5 hash algorithm (Message-Digest Algorithm 5).
        /// Note: MD5 is considered cryptographically weak and should not be used for security-critical purposes.
        /// </summary>
        MD5,

        /// <summary>
        /// Represents the SHA-1 (Secure Hash Algorithm 1).
        /// Note: SHA-1 is considered deprecated for cryptographic use due to known vulnerabilities.
        /// </summary>
        SHA1,

        /// <summary>
        /// Represents the SHA-2 (Secure Hash Algorithm 2) variant with a 224-bit hash output.
        /// Provides stronger security than MD5 and SHA-1.
        /// </summary>
        SHA2224,

        /// <summary>
        /// Represents the SHA-3 (Secure Hash Algorithm 3) variant with a 224-bit hash output.
        /// A modern cryptographic standard offering resistance to collision and pre-image attacks.
        /// </summary>
        SHA3224,

        /// <summary>
        /// Represents the bcrypt hashing algorithm.
        /// Designed for securely hashing passwords and includes a built-in salt and adjustable work factor for added security.
        /// </summary>
        bcrypt,

        /// <summary>
        /// Represents the scrypt hashing algorithm.
        /// Designed to be computationally and memory-intensive, making it suitable for securely hashing passwords.
        /// </summary>
        scrypt
    }

}
