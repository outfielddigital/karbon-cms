using System.Collections.Generic;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core.Stores
{
    internal interface IContentStore : IStore<IContent>
    {
        IContent GetByUrl(string url);
        IContent GetParent(IContent content);
        IEnumerable<IContent> GetAncestors(IContent content);
        IEnumerable<IContent> GetChildren(IContent content);
        IEnumerable<IContent> GetDescendants(IContent content);

        void SyncCache();
    }
}
