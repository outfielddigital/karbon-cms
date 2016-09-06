using System;
using System.Collections.Generic;

namespace Karbon.Cms.Core.Models
{
    public interface IEntity
    {
        string RelativePath { get; set; }
        string RelativeUrl { get; set; }
        string Slug { get; set; }
        string Name { get; set; }
        string TypeName { get; set; }
        int SortOrder { get; set; }

        DateTimeOffset Created { get; set; }
        DateTimeOffset Modified { get; set; }

        IDictionary<string, string> Data { get; set; }
    }
}
