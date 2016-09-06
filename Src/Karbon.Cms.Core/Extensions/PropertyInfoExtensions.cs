using System;
using System.Linq;
using System.Reflection;

namespace Karbon.Cms.Core
{
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Gets the custom attribute.
        /// </summary>
        /// <typeparam name="TAttributeType">The type of the attribute type.</typeparam>
        /// <param name="prop">The prop.</param>
        /// <returns></returns>
        public static TAttributeType GetCustomAttribute<TAttributeType>(this PropertyInfo prop)
            where TAttributeType : Attribute
        {
            return prop.GetCustomAttributes(typeof(TAttributeType), false)
                       .FirstOrDefault() as TAttributeType;
        }
    }
}
