namespace PassWarden.Models
{
    /// <summary>
    /// Defines various naming conventions (case styles) for strings.
    /// </summary>
    public enum Case
    {
        /// <summary>
        /// Represents camelCase, where the first letter is lowercase, and subsequent words start with uppercase letters.
        /// Example: myVariableName
        /// </summary>
        Camel,

        /// <summary>
        /// Represents PascalCase, where each word starts with an uppercase letter.
        /// Example: MyVariableName
        /// </summary>
        Pascal,

        /// <summary>
        /// Represents snake_case, where words are separated by underscores and all letters are lowercase.
        /// Example: my_variable_name
        /// </summary>
        Snake,

        /// <summary>
        /// Represents SCREAMING_SNAKE_CASE, where words are separated by underscores and all letters are uppercase.
        /// Example: MY_VARIABLE_NAME
        /// </summary>
        ScreamingSnake,

        /// <summary>
        /// Represents kebab-case, where words are separated by hyphens and all letters are lowercase.
        /// Example: my-variable-name
        /// </summary>
        Kebab,

        /// <summary>
        /// Represents Train-Case, where words are separated by hyphens and each word starts with an uppercase letter.
        /// Example: My-Variable-Name
        /// </summary>
        Train,

        /// <summary>
        /// Represents dot.case, where words are separated by dots and all letters are lowercase.
        /// Example: my.variable.name
        /// </summary>
        Dot,

        /// <summary>
        /// Represents UPPERCASE, where all letters are uppercase without any separators.
        /// Example: MYVARIABLENAME
        /// </summary>
        Upper,

        /// <summary>
        /// Represents lowercase, where all letters are lowercase without any separators.
        /// Example: myvariablename
        /// </summary>
        Lower
    }

}
