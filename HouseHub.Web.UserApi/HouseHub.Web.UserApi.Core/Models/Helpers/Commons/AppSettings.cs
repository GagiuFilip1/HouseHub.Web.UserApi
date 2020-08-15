using HouseHub.Web.UserApi.Core.Rabbit.Helpers.Setup;

namespace HouseHub.Web.UserApi.Core.Models.Helpers.Commons
{
    public class AppSettings
    {
        public RabbitMqSettings RabbitMq { get; set; }

        public RabbitMqQueues RabbitMqQueues { get; set; }
    }
}