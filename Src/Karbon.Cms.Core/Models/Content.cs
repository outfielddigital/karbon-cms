using System.Collections.Generic;

namespace Karbon.Cms.Core.Models
{
    public class Content : Entity, IContent
    {
        public virtual int Depth { get; set; }

        public IEnumerable<IFile> AllFiles { get; set; }
    }
}
