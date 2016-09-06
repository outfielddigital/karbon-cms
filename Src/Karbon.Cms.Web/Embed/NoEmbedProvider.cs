using System.Collections.Generic;

namespace Karbon.Cms.Web.Embed
{
    public class NoEmbedProvider : AbstractOEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://noembed.com/embed"; }
        }

        /// <summary>
        /// Gets the markup.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string GetMarkup(string url, IDictionary<string, string> parameters)
        {
            var requestUrl = BuildRequestUrl(url, parameters);
            var obj = GetJsonResponse<SimpleJsonEmbedResponse>(requestUrl);
            return obj.html;
        }
    }
}
