using System.Text.RegularExpressions;
using System.Web;

namespace Loteria.Data.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string that has been HTML-encoded for HTTP transmission into a decoded or encoded string.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <returns>A decoded string by default; otherwise mode=1 for an encoded string.</returns>
        public static string HtmlEntities(this string str, byte mode = 0)
        {
            return mode switch
            {
                1 => HttpUtility.HtmlEncode(str),
                _ => HttpUtility.HtmlDecode(str),
            };
        }

        /// <summary>
        /// Character that invokes an alternative interpretation on the following characters in a character sequence.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Same <see langword="string"/> adding the literal escaped characters.</returns>
        public static string? EscapeString(this string input) => input == null ? null : Regex.Replace(input, @"[\r\n\x00\x1a\\'""]", @"\$0");

        /// <summary>
        /// Find and remove any special char, only letting numbers and letters in.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Same <see langword="string"/> without any special char.</returns>
        public static string? RemoveSpecialChars(this string input) => input == null ? null : Regex.Replace(input, @"[^0-9a-zA-Z]", " ");
    }
}
