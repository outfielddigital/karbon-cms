using System.Web.Mvc;
using Karbon.Cms.Core;
using Karbon.Cms.Core.Models;
using System.Text;
using System;
using Karbon.Cms.Web.Models;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Globalization;

namespace Karbon.Cms.Web.Controllers
{
    public class KarbonUtilityController : Controller
    {
        /// <summary>
        /// Gets the home page.
        /// </summary>
        /// <value>
        /// The home page.
        /// </value>
        public IContent HomePage
        {
            get { return KarbonWebContext.Current.HomePage; }
        }

        public virtual ActionResult Robot()
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine("User-agent: *");
            content.AppendLine("Disallow:");
            content.AppendLine(String.Empty);
            content.AppendLine(String.Format("Sitemap: {0}/sitemap.xml", KarbonWebContext.Current.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority)));

            return this.Content(content.ToString(), "text/plain", Encoding.UTF8);
        }

        public virtual ActionResult Sitemap()
        {
            var allDescendants = HomePage.Descendants(child => child.IsVisible());

            List<SitemapNode> sitemapNodes = new List<SitemapNode>();

            sitemapNodes.Add(
                   new SitemapNode()
                   {
                       Url = HomePage.Url(),
                       Frequency = SitemapFrequency.Daily,
                       Priority = 1
                   });

            foreach (IContent item in allDescendants)
            {
                sitemapNodes.Add(
                   new SitemapNode()
                   {
                       Url = item.Url(),
                       Frequency = SitemapFrequency.Daily,
                       Priority = 0.9
                   });
            }

            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);

            return this.Content(document.ToString(), "text/xml", Encoding.UTF8);
        }

    }
}
