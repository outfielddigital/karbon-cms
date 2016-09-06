using System;
using System.IO;
using System.Linq;

namespace Karbon.Cms.Core
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Ensures a trailing directory separator.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string EnsureTrailingDirectorySeparator(this string path)
        {
            return EnsureTrailingCharacter(path, Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Ensures a trailing forward slash.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string EnsureTrailingForwardSlash(this string path)
        {
            var url = EnsureTrailingCharacter(path, '/');
            if (url == "~//")
                url = "~/";

            return url;
        }

        /// <summary>
        /// Ensures a trailing character.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="character">The character.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">input</exception>
        public static string EnsureTrailingCharacter(this string input, char character)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            return input.Length == 0 || input[input.Length - 1] == character 
                ? input 
                : input + character;
        }

        /// <summary>
        /// Trims the start of a string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="toTrim">To trim.</param>
        /// <returns></returns>
        public static string TrimStart(this string input, string toTrim)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(toTrim))
                return input;

            while (input.StartsWith(toTrim, StringComparison.InvariantCultureIgnoreCase))
                input = input.Substring(toTrim.Length);

            return input;
        }

        /// <summary>
        /// Trims the end of a string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="toTrim">To trim.</param>
        /// <returns></returns>
        public static string TrimEnd(this string input, string toTrim)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(toTrim)) 
                return input;

            while (input.EndsWith(toTrim, StringComparison.InvariantCultureIgnoreCase))
                input = input.Remove(input.LastIndexOf(toTrim, StringComparison.InvariantCultureIgnoreCase));

            return input;
        }

        /// <summary>
        /// Determines whether the input string is alpha numeric.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the input string is alpha numeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlphaNumeric(this string input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            return input.All(char.IsLetterOrDigit);
            // return Regex.IsMatch(input, @"^[a-zA-Z0-9]*$");
        }
    }
}
