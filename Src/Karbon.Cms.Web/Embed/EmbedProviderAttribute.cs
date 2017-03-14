using System;

namespace Karbon.Cms.Web.Embed
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EmbedProviderAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL scheme regex.
        /// </summary>
        /// <value>
        /// The URL scheme regex.
        /// </value>
        public string UrlSchemeRegex { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedProviderAttribute" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="urlSchemeRegex">The URL scheme regex.</param>
        public EmbedProviderAttribute(string name, string urlSchemeRegex)
        {
            Name = name;
            UrlSchemeRegex = urlSchemeRegex;
        }
    }
}
