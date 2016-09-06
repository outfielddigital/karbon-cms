using System.Collections.Generic;
using System.Web;

namespace Karbon.Cms.Web.Embed
{
    public abstract class AbstractPhotoOEmbedProvider : AbstractOEmbedProvider
    {
        /// <summary>
        /// Gets the markup.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string GetMarkup(string url, IDictionary<string, string> parameters)
        {
            var requestUrl = BuildRequestUrl(url, parameters);
            var doc = GetXmlResponse(requestUrl);

            string imageUrl = doc.SelectSingleNode("/oembed/url").InnerText;
            string imageWidth = doc.SelectSingleNode("/oembed/width").InnerText;
            string imageHeight = doc.SelectSingleNode("/oembed/height").InnerText;
            string imageTitle = doc.SelectSingleNode("/oembed/title").InnerText;

            return string.Format("<img src=\"{0}\" width\"{1}\" height=\"{2}\" alt=\"{3}\" />",
                imageUrl, imageWidth, imageHeight, HttpUtility.HtmlEncode(imageTitle));
        }
    }
}
