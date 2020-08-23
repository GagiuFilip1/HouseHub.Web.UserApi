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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext m_dataContext;

        public CustomerRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(Customer entity)
        {
            await m_dataContext.Customers.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<Customer> entities)
        {
            m_dataContext.Customers.UpdateRange(entities);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer entity)
        {
            m_dataContext.Customers.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task<Tuple<int, List<Customer>>> SearchAsync(Pagination pagination, Ordering ordering, IFilter<Customer> filtering)
        {
            return await filtering.Filter(m_dataContext.Customers.AsQueryable())
                .WithOrdering(ordering, new Ordering())
                .WithPaginationAsync(pagination);
        }
    }
}