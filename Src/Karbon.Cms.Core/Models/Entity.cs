using System;
using System.Collections.Generic;

namespace Karbon.Cms.Core.Models
{
    public abstract class Entity : IEntity
    {
        public virtual string RelativePath { get; set; }
        public virtual string RelativeUrl { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Name { get; set; }
        public virtual string TypeName { get; set; }
        public virtual int SortOrder { get; set; }

        public virtual DateTimeOffset Created { get; set; }
        public virtual DateTimeOffset Modified { get; set; }

        /// <summary>
        /// <para>Gets or sets the data for this content.</para>
        /// <para>The Data property gives you raw access to the data values. If you want to make use
        /// of inbuilt error checking and fallback values, concider using one of the
        /// .Get() extension methods instead.</para>
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public virtual IDictionary<string, string> Data { get; set; }

        protected Entity()
        {
            Data = new Dictionary<string, string>();
        }
    }
}
