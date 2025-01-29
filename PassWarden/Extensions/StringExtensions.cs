namespace PassWarden.Extensions
{
    internal static class StringExtensions
    {
        internal static string GetAsCamel(this string input) => 
            input[0].ToString().ToLower() + input[1..].Replace(" ", string.Empty);

        internal static string GetAsPascal(this string input) => 
            input[0].ToString().ToUpper() + input[1..].Replace(" ", string.Empty);

        internal static string GetAsSnake(this string input) =>
            input[0].ToString().ToLower() + input[1..].Replace(" ", "_");

        internal static string GetAsScreamingSnake(this string input) => 
            input.Replace(" ", "_").ToUpper();

        internal static string GetAsKebabOrTrain(this string input) => 
            input.Replace(" ", "-");

        internal static string GetAsDot(this string input) => 
            input.Replace(" ", ".");

        internal static string GetAsUpper(this string input) => 
            input.ToUpper().Replace(" ", string.Empty);

        internal static string GetAsLower(this string input) => 
            input.ToLower().Replace(" ", string.Empty);
    }
}
