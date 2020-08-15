using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseHub.Web.UserApi.Core.Enums;
using HouseHub.Web.UserApi.Core.Rabbit.Helpers.Responses;
using HouseHub.Web.UserApi.Core.Rabbit.Helpers.Setup;
using HouseHub.Web.UserApi.Core.Rabbit.Models;

namespace HouseHub.Web.UserApi.Core.Rabbit.Interfaces
{
    public interface IRabbitHandler
    {
        public Dictionary<MessageType, Func<string, Task<RabbitResponse>>> Processors { get; }
        T PublishRpc<T>(PublishOptions options);
        void PublishRpc(PublishOptions options);
        void Publish(PublishOptions options);
        void ListenQueueAsync(ListenOptions options);
        void DeclareRpcQueue(QueueSetup setup);
        void DeclareQueue(QueueSetup queueSetup);
    }
}