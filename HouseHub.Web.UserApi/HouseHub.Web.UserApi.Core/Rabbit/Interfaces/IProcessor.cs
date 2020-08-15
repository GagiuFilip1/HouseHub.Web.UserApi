using System.Threading.Tasks;
using HouseHub.Web.UserApi.Core.Enums;
using HouseHub.Web.UserApi.Core.Rabbit.Helpers.Responses;

namespace HouseHub.Web.UserApi.Core.Rabbit.Interfaces
{
    public interface IProcessor
    {
        public MessageType Type { get; set; }
        public Task<RabbitResponse> ProcessAsync(string data);
    }
}