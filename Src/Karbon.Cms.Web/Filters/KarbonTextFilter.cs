using System.IO;
using System.Text;
using Karbon.Cms.Web.Parsers.KarbonText;

namespace Karbon.Cms.Web.Filters
{
    /// <summary>
    /// A response filter to parse karbon text tags
    /// </summary>
    internal class KarbonTextFilter : MemoryStream
    {
        private readonly Stream _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonTextFilter"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public KarbonTextFilter(Stream response)
        {
            _response = response;
        }

        /// <summary>
        /// Writes a block of bytes to the current stream using data read from a buffer.
        /// </summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            var html = Encoding.UTF8.GetString(buffer);
            html = ReplaceTags(html);
            buffer = Encoding.UTF8.GetBytes(html);
            _response.Write(buffer, offset, count);
        }

        /// <summary>
        /// Replaces the tags.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private string ReplaceTags(string html)
        {
            if (KarbonTextParser.Instance == null 
                || KarbonWebContext.Current == null 
                || KarbonWebContext.Current.CurrentPage == null)
                return html;

            return KarbonTextParser.Instance.ParseTags(KarbonWebContext.Current.CurrentPage, html);
        }
    }
}
