using System.Collections.Generic;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("Blip", @"blip\.tv/")]
    public class BlipEmbedProvider : AbstractVideoOEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://blip.tv/oembed/"; }
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
