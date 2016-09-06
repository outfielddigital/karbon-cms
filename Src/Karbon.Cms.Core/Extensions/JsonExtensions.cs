using System.Web.Script.Serialization;

namespace Karbon.Cms.Core
{
    internal static class JsonExtensions
    {
        /// <summary>
        /// Serializes to json.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string SerializeToJson(this object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        /// <summary>
        /// Deserializes the json to to specified object type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public static TEntity DeserializeJsonTo<TEntity>(this string json)
        {
            return new JavaScriptSerializer().Deserialize<TEntity>(json);
        }
    }
}
