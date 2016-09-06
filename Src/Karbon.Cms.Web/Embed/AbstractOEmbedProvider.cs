using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Karbon.Cms.Core;

namespace Karbon.Cms.Web.Embed
{
    public abstract class AbstractOEmbedProvider : AbstractEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public abstract string ApiEndpoint { get; }

        /// <summary>
        /// Gets the default parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public virtual IDictionary<string, string> Parameters 
        { 
            get { return new Dictionary<string, string>(); }
        }

        /// <summary>
        /// Gets the markup.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param> 
        /// <returns></returns>
        public override string GetMarkup(string url, IDictionary<string, string> parameters)
        { 
            var requestUrl = BuildRequestUrl(url, parameters);
            var responseXml = GetXmlResponse(requestUrl);
            var selectSingleNode = responseXml.SelectSingleNode("/oembed/html");

            return selectSingleNode != null 
                ? selectSingleNode.InnerText 
                : null;
        }

        /// <summary>
        /// Builds the request URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected virtual string BuildRequestUrl(string url, IDictionary<string, string> parameters)
        {
            foreach (var p in Parameters.Where(p => !parameters.ContainsKey(p.Key)))
                parameters.Add(p.Key, p.Value);

            parameters.Add("url", url);

            return ApiEndpoint + parameters.ToQueryString();
        }

        /// <summary>
        /// Gets the XML response.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        protected virtual XmlDocument GetXmlResponse(string url)
        {
            var response = GetStringResponse(url);

            var doc = new XmlDocument();
            doc.LoadXml(response);

            return doc;
        }

        /// <summary>
        /// Gets the json response.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        protected virtual TEntity GetJsonResponse<TEntity>(string url)
        {
            var response = GetStringResponse(url);
            return response.DeserializeJsonTo<TEntity>();
        }

        /// <summary>
        /// Gets the string response.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        protected virtual string GetStringResponse(string url)
        {
            var webClient = new System.Net.WebClient();
            return webClient.DownloadString(url);
        }
    }
}
