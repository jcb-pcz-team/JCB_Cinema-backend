using System.Text.RegularExpressions;

namespace JCB_Cinema.Tools
{
    public static class StringExtensions
    {
        public static string NormalizeString(this string str, char from = ' ', char to = '-')
        {
            return Regex.Replace(str, "[^a-zA-Z0-9 ]", "").ToLower().Replace(from, to);
        }
    }
}
