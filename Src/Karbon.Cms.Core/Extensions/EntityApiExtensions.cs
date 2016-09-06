using System;
using System.Collections.Generic;
using System.ComponentModel;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core
{
    public static class EntityApiExtensions
    {
        /// <summary>
        /// Gets the value for the given key.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string Get(this IEntity content, string key, string defaultValue = "")
        {
            string value;
            return content.TryGet(key, out value) && !string.IsNullOrEmpty(value)
                ? value
                : defaultValue;
        }

        /// <summary>
        /// Gets the value for the given key.
        /// </summary>
        /// <typeparam name="TValueType">The type of the value type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static TValueType Get<TValueType>(this IEntity content, string key, TValueType defaultValue = default(TValueType))
        {
            TValueType value;
            return content.TryGet(key, out value) && !EqualityComparer<TValueType>.Default.Equals(value, default(TValueType))
                ? value
                : defaultValue;
        }

        /// <summary>
        /// Gets the value for the given key.
        /// </summary>
        /// <typeparam name="TValueType">The type of the value type.</typeparam>
        /// <typeparam name="TConverterType">The type of the converter type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static TValueType Get<TValueType, TConverterType>(this IEntity content, string key, TValueType defaultValue = default(TValueType))
            where TConverterType : TypeConverter
        {
            TValueType value;
            return content.TryGet<TValueType, TConverterType>(key, out value) && !EqualityComparer<TValueType>.Default.Equals(value, default(TValueType))
                ? value
                : defaultValue;
        }

        /// <summary>
        /// Tries to get the value for the given key.
        /// </summary>
        /// <typeparam name="TValueType">The type of the value type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool TryGet<TValueType>(this IEntity content, string key, out TValueType value)
        {
            value = default(TValueType);

            //var prop = content.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            //    .SingleOrDefault(x => x.Name.ToLower(CultureInfo.InvariantCulture) == key.ToLower(CultureInfo.InvariantCulture) 
            //        && x.PropertyType == typeof (TValueType) 
            //        && x.CanRead);

            //if(prop != null)
            //{
            //    value = (TValueType)prop.GetValue(content, null);
            //    return true;
            //}

            if (!content.Data.ContainsKey(key))
                return false;

            try
            {
                var tmpValue = content.Data[key];
                var converter = TypeDescriptor.GetConverter(typeof(TValueType));
                value = (TValueType)converter.ConvertFromString(tmpValue);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to get the value for the given key.
        /// </summary>
        /// <typeparam name="TValueType">The type of the value type.</typeparam>
        /// <typeparam name="TConverterType">The type of the converter type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool TryGet<TValueType, TConverterType>(this IEntity content, string key, out TValueType value)
            where TConverterType : TypeConverter
        {
            value = default(TValueType);

            if (!content.Data.ContainsKey(key))
                return false;

            try
            {
                var tmpValue = content.Data[key];
                var converter = Activator.CreateInstance(typeof(TConverterType)) as TypeConverter;
                if (converter == null)
                    return false;

                value = (TValueType)converter.ConvertFromString(tmpValue);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
