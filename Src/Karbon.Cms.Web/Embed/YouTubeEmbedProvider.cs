using System.Collections.Generic;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("YouTube", @"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)")]
    public class YouTubeEmbedProvider : AbstractVideoOEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://www.youtube.com/oembed"; }
        }

        /// <summary>
        /// Gets the default parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public override IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {"iframe", "1"},
                    {"format", "xml"}
                };
            }
        }
    }
}
