using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core.Stores
{
    internal interface IStore<TEntity>
        where TEntity : IEntity
    { }
}
