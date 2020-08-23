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
    public class DomainRepository : IDomainRepository
    {
        private readonly DataContext m_dataContext;

        public DomainRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(Domain entity)
        {
            await m_dataContext.Domains.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<Domain> entities)
        {
            m_dataContext.Domains.UpdateRange(entities);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Domain entity)
        {
            m_dataContext.Domains.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task<Tuple<int, List<Domain>>> SearchAsync(Pagination pagination, Ordering ordering, IFilter<Domain> filtering)
        {
            return await filtering.Filter(m_dataContext.Domains.AsQueryable())
                .WithOrdering(ordering, new Ordering())
                .WithPaginationAsync(pagination);
        }
    }
}