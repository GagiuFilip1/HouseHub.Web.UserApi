using System.Linq;
using HouseHub.Web.UserApi.Core.Models.Interfaces.Commons;

namespace HouseHub.Web.UserApi.Core.Models.Filters.Interfaces
{
    public interface IFilter<TEntity> where TEntity : IIdentifier
    {
        public string SearchTerm { get; set; }
        IQueryable<TEntity> Filter(IQueryable<TEntity> filterQuery);
    }
}