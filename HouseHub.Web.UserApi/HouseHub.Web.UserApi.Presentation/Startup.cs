using GraphQL.Server;
using GraphQL.Types;
using HouseHub.Web.UserApi.Core.Models.Helpers.Commons;
using HouseHub.Web.UserApi.Infrastructure.Data;
using HouseHub.Web.UserApi.Infrastructure.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HouseHub.Web.UserApi.Presentation
{
    public class Startup
    {
        private const string CONNECTION_STRING_PATH = "ConnectionStrings:UserApi";
        private const string MIGRATION_ASSEMBLY = "HouseHub.Web.UserApi.Presentation";
        private const string REPOSITORIES_NAMESPACE = "HouseHub.Web.UserApi.Infrastructure.Repositories.Implementations";
        private const string SERVICES_NAMESPACE = "HouseHub.Web.UserApi.Core.Services.Implementations";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.InjectMySqlDbContext<DataContext>(Configuration[CONNECTION_STRING_PATH], MIGRATION_ASSEMBLY);
            services.InjectForNamespace(REPOSITORIES_NAMESPACE);
            services.InjectForNamespace(SERVICES_NAMESPACE);
            services.InjectRabbitMq();
            services.InjectGraphQl();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGraphQL<ISchema>();
        }
    }
}