using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using Karbon.Cms.Web.Embed;
using Karbon.Cms.Web.Parsers.KarbonText;
using MarkdownDeep;

namespace Karbon.Cms.Web
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Converts markdown content to HTML.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static IHtmlString Markdown(this HtmlHelper helper, string input)
        {
            return new HtmlString(new Markdown().Transform(input));
        }

        /// <summary>
        /// Converts markdown content to HTML.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static IHtmlString Md(this HtmlHelper helper, string input)
        {
            return helper.Markdown(input);
        }

        /// <summary>
        /// Outputs the given Markdown string as KarbonText.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="markdown">Markdown text.</param>
        /// <returns>The processed HTML</returns>
        public static IHtmlString KarbonText(this HtmlHelper helper, string markdown)
        {
            return new HtmlString(KarbonTextParser.Instance.ParseTags(KarbonWebContext.Current.CurrentPage ?? KarbonWebContext.Current.HomePage, helper.Markdown(markdown).ToHtmlString()));
        }

        /// <summary>
        /// Outputs the given Markdown string as KarbonText.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="html">HTML to parse.</param>
        /// <returns>The processed HTML</returns>
        public static IHtmlString KarbonText(this HtmlHelper helper, IHtmlString html)
        {
            return new HtmlString(KarbonTextParser.Instance.ParseTags(KarbonWebContext.Current.CurrentPage ?? KarbonWebContext.Current.HomePage, html.ToHtmlString()));
        }

        /// <summary>
        /// Converts new lines to HTML breaks.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static IHtmlString Multiline(this HtmlHelper helper, string input)
        {
            return new HtmlString(input.Replace(Environment.NewLine, "<br />"));
        }

        /// <summary>
        /// Extracts a short excerpt from the input string.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns></returns>
        public static IHtmlString Excerpt(this HtmlHelper helper, string input, int length = 150, string suffix = "...")
        {
            if(input == null)
                input = "";

            if (input.Length > length - suffix.Length)
                input = input.Substring(0, length - suffix.Length) + suffix;

            return new HtmlString(input);
        }

        /// <summary>
        /// Embeds the specified URL via oEmbed.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <param name="maxHeight">Height of the max.</param>
        /// <returns></returns>
        public static IHtmlString Embed(this HtmlHelper helper, string url, int maxWidth = 0, int maxHeight = 0)
        {
            var parameters = new Dictionary<string, string>();

            if(maxWidth > 0)
                parameters.Add("maxwidth", maxWidth.ToString(CultureInfo.InvariantCulture));

            if (maxHeight > 0)
                parameters.Add("maxheight", maxHeight.ToString(CultureInfo.InvariantCulture));

            return new HtmlString(EmbedProviderFactory.Instance.GetMarkup(url, parameters));
        }


        /// <summary>
        /// Embeds the specified URL.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static IHtmlString Embed(this HtmlHelper helper, string url, IDictionary<string, string> parameters)
        {
            return new HtmlString(EmbedProviderFactory.Instance.GetMarkup(url, parameters));
        }
    }
}
