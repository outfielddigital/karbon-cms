using System.Collections.Generic;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("Viddler", @"viddler\.com/explore/.*/videos/\w+/?")]
    public class ViddlerEmbedProvider : AbstractVideoOEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://lab.viddler.com/services/oembed/"; }
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
                    {"format", "xml"}
                };
            }
        }
    }
}
