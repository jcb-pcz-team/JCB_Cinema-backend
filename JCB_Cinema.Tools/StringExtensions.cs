using System.Text.RegularExpressions;

namespace JCB_Cinema.Tools
{
    /// <summary>
    /// Extension methods for manipulating and normalizing strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Normalizes a string by removing non-alphanumeric characters, converting it to lowercase, and replacing specified characters.
        /// </summary>
        /// <param name="str">The string to normalize.</param>
        /// <param name="from">The character to be replaced (default is space).</param>
        /// <param name="to">The character to replace the <paramref name="from"/> character with (default is hyphen).</param>
        /// <returns>A normalized string with alphanumeric characters only, converted to lowercase, and replaced specified characters.</returns>
        public static string NormalizeString(this string str, char from = ' ', char to = '-')
        {
            // Remove non-alphanumeric characters and convert to lowercase, then replace the 'from' character with the 'to' character
            return Regex.Replace(str, "[^a-zA-Z0-9 ]", "").ToLower().Replace(from, to);
        }
    }
}
