using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseHub.Web.UserApi.Core.Extensions;
using HouseHub.Web.UserApi.Core.Models.Entities;
using HouseHub.Web.UserApi.Core.Models.Filters.Interfaces;
using HouseHub.Web.UserApi.Core.Models.Helpers.Commons;
using HouseHub.Web.UserApi.Core.Repositories.Interfaces;
using HouseHub.Web.UserApi.Infrastructure.Data;
using HouseHub.Web.UserApi.Infrastructure.Ioc;

namespace HouseHub.Web.UserApi.Infrastructure.Repositories.Implementations
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class LocationRepository : ILocationRepository
    {
        private readonly DataContext m_dataContext;

        public LocationRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(Location entity)
        {
            await m_dataContext.Locations.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<Location> entities)
        {
            m_dataContext.Locations.UpdateRange(entities);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Location entity)
        {
            m_dataContext.Locations.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task<Tuple<int, List<Location>>> SearchAsync(Pagination pagination, Ordering ordering, IFilter<Location> filtering)
        {
            return await filtering.Filter(m_dataContext.Locations.AsQueryable())
                .WithOrdering(ordering, new Ordering())
                .WithPaginationAsync(pagination);
        }
    }
}