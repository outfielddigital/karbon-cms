using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karbon.Cms.Web
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Converts the dictionary to a valid querystring.
        /// </summary>
        /// <param name="dict">The dict.</param>
        /// <returns></returns>
        internal static string ToQueryString(this IEnumerable<KeyValuePair<string, object>> dict)
        {
            return dict.ToDictionary(x => x.Key, x => x.Value.ToString())
                .ToQueryString();
        }

        /// <summary>
        /// Converts the dictionary to a valid querystring.
        /// </summary>
        /// <param name="dict">The dict.</param>
        /// <returns></returns>
        internal static string ToQueryString(this IEnumerable<KeyValuePair<string, string>> dict)
        {
            if (dict == null || !dict.Any())
                return "";

            return "?" + string.Join("&", dict.Select(x => string.Format("{0}={1}",
                HttpUtility.UrlEncode(x.Key),
                HttpUtility.UrlEncode(x.Value))));
        }
    }
}
