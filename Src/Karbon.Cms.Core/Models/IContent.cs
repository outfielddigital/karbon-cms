using System.Collections.Generic;

namespace Karbon.Cms.Core.Models
{
    public interface IContent : IEntity
    {
        int Depth { get; set; }

        IEnumerable<IFile> AllFiles { get; set; }
    }
}
