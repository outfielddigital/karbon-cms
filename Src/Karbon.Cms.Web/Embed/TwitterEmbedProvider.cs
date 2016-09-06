namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("Twitter", @"twitter\.com/")]
    public class TwitterEmbedProvider : AbstractRichOEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "https://api.twitter.com/1/statuses/oembed.xml"; }
        }
    }
}
