using System.Collections.Generic;
using System.Text;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("Gist", @"gist\.github\.com/")]
    public class GistEmbedProvider : AbstractEmbedProvider
    {
        /// <summary>
        /// Gets the markup.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string GetMarkup(string url, IDictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            sb.Append("<script src=\"");
            sb.Append(url);
            sb.Append(".js");

            if (parameters.ContainsKey("file"))
                sb.AppendFormat("?file={0}", parameters["file"]);

            sb.Append("\"></script>");
            return sb.ToString();
        }
    }
}
