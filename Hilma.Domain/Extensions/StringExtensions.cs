using System.Linq;
using System.Text.RegularExpressions;

namespace Hilma.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool HasAnyContent( this string[] array )
        {
            return array != null && array.Any(value => !string.IsNullOrEmpty(value));
        }

        /// <summary>
        /// Eg. http://http://www.com -> http://www.com
        /// </summary>
        /// <param name="url">The url</param>
        /// <returns>Cleaned url</returns>
        public static string CleanUrl(this string url)
        {
            var delimiter = "://";
            if (!string.IsNullOrEmpty(url) && Regex.Matches(url, delimiter).Count > 1)
            {
                return url.Substring(url.Substring(0, url.LastIndexOf(delimiter)).LastIndexOf(delimiter) + 3);
            }
            else
            {
                return url;
            }
        }
    }
}
