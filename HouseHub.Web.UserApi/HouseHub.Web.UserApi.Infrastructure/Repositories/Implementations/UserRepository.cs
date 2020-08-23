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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext m_dataContext;

        public UserRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(User entity)
        {
            await m_dataContext.Users.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<User> entities)
        {
            m_dataContext.Users.UpdateRange(entities);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            m_dataContext.Users.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task<Tuple<int, List<User>>> SearchAsync(Pagination pagination, Ordering ordering, IFilter<User> filtering)
        {
            return await filtering.Filter(m_dataContext.Users.AsQueryable())
                .WithOrdering(ordering, new Ordering())
                .WithPaginationAsync(pagination);
        }
    }
}