using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseHub.Web.UserApi.Core.Models.Interfaces.Commons
{
    public interface IValidEntityAsync
    { 
        Task<List<string>> ValidateAsync();
    }
}