namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("Vimeo", @"vimeo\.com/")]
    public class VimeoEmbedProvider : AbstractVideoOEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://vimeo.com/api/oembed.xml"; }
        }
    }
}
