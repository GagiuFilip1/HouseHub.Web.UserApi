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
    public class WorkerRepository : IWorkerRepository
    {
        private readonly DataContext m_dataContext;

        public WorkerRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(Worker entity)
        {
            await m_dataContext.Workers.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<Worker> entities)
        {
            m_dataContext.Workers.UpdateRange(entities);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Worker entity)
        {
            m_dataContext.Workers.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task<Tuple<int, List<Worker>>> SearchAsync(Pagination pagination, Ordering ordering, IFilter<Worker> filtering)
        {
            return await filtering.Filter(m_dataContext.Workers.AsQueryable())
                .WithOrdering(ordering, new Ordering())
                .WithPaginationAsync(pagination);
        }
    }
}