using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core.Mapping
{
    internal class DataMapper
    {
        /// <summary>
        /// Maps the specified data to an entity.
        /// </summary>
        /// <typeparam name="TEntityType">The entity type.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public TEntityType Map<TEntityType>(TEntityType entity, IDictionary<string, string> data)
            where TEntityType : IEntity
        {
            return (TEntityType) Map(typeof (TEntityType), entity, data);
        }

        /// <summary>
        /// Maps the specified data to an entity.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">entityType</exception>
        public object Map(Type entityType, object entity, IDictionary<string, string> data)
        {
            if(!typeof(IEntity).IsAssignableFrom(entityType))
                throw new ArgumentException("entityType");

            var props = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dataKey in data.Keys)
            {
                var prop = props.SingleOrDefault(x => x.Name == dataKey && x.CanWrite);
                if (prop != null)
                {
                    // Found property, so attempt to set it
                    TypeConverter typeConverter = null;
                    var typeConverterAttr = prop.GetCustomAttribute<TypeConverterAttribute>();
                    if(typeConverterAttr != null)
                    {
                        var typeConverterType = Type.GetType(typeConverterAttr.ConverterTypeName);
                        if (typeConverterType != null)
                            typeConverter = Activator.CreateInstance(typeConverterType) as TypeConverter;
                    }

                    if (typeConverter == null)
                        typeConverter = TypeDescriptor.GetConverter(prop.PropertyType);

                    var propValue = typeConverter.ConvertFromString(data[dataKey]);
                    prop.SetValue(entity, propValue, null);
                }
                else
                {
                    // No property so add to data collection
                    if (((IEntity)entity).Data.ContainsKey(dataKey))
                        ((IEntity)entity).Data[dataKey] = data[dataKey];
                    else
                        ((IEntity)entity).Data.Add(dataKey, data[dataKey]);
                }
            }

            return entity;
        }
    }
}
