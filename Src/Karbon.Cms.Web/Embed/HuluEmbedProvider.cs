using System.Collections.Generic;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("Hulu", @"hulu\.com/")]
    public class HuluEmbedProvider : AbstractVideoOEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://www.hulu.com/api/oembed.xml"; }
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
                    { "format", "xml" }
                };
            }
        }
    }
}
