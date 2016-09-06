using System;
using System.Linq;
using System.Reflection;

namespace Karbon.Cms.Core
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the specified type has a given method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>
        ///   <c>true</c> if the specified type has the given method; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasMethod(this Type type, string methodName)
        {
            return type.HasMethod(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, methodName);
        }

        /// <summary>
        /// Determines whether the specified type has a given method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>
        ///   <c>true</c> if the specified type has the given method; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasMethod(this Type type, BindingFlags bindingFlags, string methodName)
        {
            return type.GetMethods(bindingFlags)
                .Any(x => x.Name.ToLower() == methodName.ToLower());
        }

        /// <summary>
        /// Determines whether the given type is assignable from the specified base type.
        /// </summary>
        /// <param name="baseType">The base type.</param>
        /// <param name="extendType">The type.</param>
        /// <returns>
        ///   <c>true</c> if is assignable from the specified base type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAssignableFromExtended(this Type baseType, Type extendType)
        {
            while (!baseType.IsAssignableFrom(extendType))
            {
                if (extendType.Equals(typeof(object)))
                {
                    return false;
                }
                if (extendType.IsGenericType && !extendType.IsGenericTypeDefinition)
                {
                    extendType = extendType.GetGenericTypeDefinition();
                }
                else
                {
                    extendType = extendType.BaseType;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the custom attribute.
        /// </summary>
        /// <typeparam name="TAttributeType">The type of the attribute type.</typeparam>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static TAttributeType GetCustomAttribute<TAttributeType>(this Type type)
            where TAttributeType : Attribute
        {
            return type.GetCustomAttributes(typeof(TAttributeType), false)
                       .FirstOrDefault() as TAttributeType;
        }
    }
}
