using System;
using System.Linq;

namespace Neitzel
{
    /// <summary>
    /// Usefull extensions to the String class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if the source string contains any of the given characters.
        /// </summary>
        /// <param name="source">Source string to search in.</param>
        /// <param name="characters">Array of characters to search for.</param>
        /// <returns>True if a character was inside the source string, else false.</returns>
        public static bool Contains(this string source, char[] characters)
        {
            // validate
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (characters == null) throw new ArgumentNullException(nameof(characters));

            return characters.Any(source.Contains);
        }

        /// <summary>
        /// Gets the first occurance of any given character inside the source string.
        /// </summary>
        /// <param name="source">Source string to search in.</param>
        /// <param name="characters">Characters to look for.</param>
        /// <returns>First index of a found char or -1 if no given character was found.</returns>
        public static int FirstIndexOf(this string source, char[] characters)
        {
            // validate
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (characters == null) throw new ArgumentNullException(nameof(characters));

            var first = int.MaxValue;
            foreach (var ch in characters)
            {
                var cur = source.IndexOf(ch);
                if (cur != -1 && cur < first)
                    first = cur;

            }

            return (first == int.MaxValue) ? -1 : first;
        }
    }
}
