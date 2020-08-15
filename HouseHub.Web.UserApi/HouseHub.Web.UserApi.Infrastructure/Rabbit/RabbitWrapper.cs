using System.Collections.Generic;
using HouseHub.Web.UserApi.Core.Models.Helpers.Commons;
using HouseHub.Web.UserApi.Core.Rabbit.Helpers.Setup;
using HouseHub.Web.UserApi.Core.Rabbit.Interfaces;
using HouseHub.Web.UserApi.Infrastructure.Ioc;
using Microsoft.Extensions.Options;

namespace HouseHub.Web.UserApi.Infrastructure.Rabbit
{
    [RegistrationKind(Type = RegistrationType.Scoped, AsSelf = true)]
    public class RabbitWrapper
    {
        private readonly AppSettings m_appSettings;
        private readonly IRabbitHandler m_handler;
        private readonly IEnumerable<IProcessor> m_processors;

        public RabbitWrapper(IOptions<AppSettings> appSettings, IRabbitHandler handler, IEnumerable<IProcessor> processors)
        {
            m_handler = handler;
            m_appSettings = appSettings.Value;
            m_processors = processors;
            SetProcessors();
            InitialiseQueues();
        }

        private void InitialiseQueues()
        {
          
        }

        private void SetProcessors()
        {
            foreach (var processor in m_processors) m_handler.Processors.TryAdd(processor.Type, processor.ProcessAsync);
        }
    }
}