using System.Collections.Generic;

namespace HouseHub.Web.UserApi.Core.Models.Interfaces.Commons
{
    public interface IValidEntity
    {
        List<string> Validate();
    }
}