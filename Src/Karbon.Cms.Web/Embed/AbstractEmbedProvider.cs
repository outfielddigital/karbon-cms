using System.Collections.Generic;

namespace Karbon.Cms.Web.Embed
{
    public abstract class AbstractEmbedProvider
    {
        /// <summary>
        /// Gets the markup.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public abstract string GetMarkup(string url, IDictionary<string, string> parameters);
    }
}
