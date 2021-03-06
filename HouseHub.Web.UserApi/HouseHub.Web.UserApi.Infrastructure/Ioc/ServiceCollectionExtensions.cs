﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.Utilities;
using HouseHub.Web.UserApi.Core.Extensions;
using HouseHub.Web.UserApi.Core.GraphQl.Root;
using HouseHub.Web.UserApi.Core.Models.Helpers.Commons;
using HouseHub.Web.UserApi.Infrastructure.Rabbit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace HouseHub.Web.UserApi.Infrastructure.Ioc
{
    public static class ServiceCollectionExtensions
    {
        private const string SOLUTION_NAME = "HouseHub";

        static ServiceCollectionExtensions()
        {
            Assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(t => t.FullName.Contains(SOLUTION_NAME))
                .ToList();
        }

        private static IServiceCollection Services { get; set; }
        private static List<Assembly> Assemblies { get; }

        public static void InjectMySqlDbContext<TContext>(this IServiceCollection collection, string connectionString, string assembly) where TContext : DbContext
        {
            collection.AddDbContext<TContext>(options =>
                options.UseMySql(connectionString,
                    b => b.MigrationsAssembly(assembly)
                        .ServerVersion(new ServerVersion(new Version(5, 7, 12)))
                        .CharSet(CharSet.Latin1)
                )
            );
        }

        public static void InjectForNamespace(this IServiceCollection collection, string nameSpace)
        {
            Services = collection;
            InjectDependenciesForAssembly(Assemblies.FirstOrDefault(t => nameSpace.Contains(t.GetName().Name)), nameSpace);
        }

        private static void InjectDependenciesForAssembly(Assembly assembly, string nameSpace)
        {
            if (assembly == null)
                return;

            var typesToRegister = assembly.GetTypes()
                .Where(x => x.Namespace != null && x.Namespace.Contains(nameSpace, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
            typesToRegister.ForEach(RegisterType);
        }

        private static void RegisterType(Type typeToRegister)
        {
            var registrationType = typeToRegister.GetCustomAttribute<RegistrationKindAttribute>();
            if (registrationType == null)
            {
                if (typeToRegister.GetInterfaces().Any())
                    Services.AddScoped(typeToRegister.GetInterfaces().First(), typeToRegister);
                return;
            }


            switch (registrationType.Type)
            {
                case RegistrationType.Singleton:
                    if (registrationType.AsSelf)
                        Services.AddSingleton(typeToRegister);
                    else
                        Services.AddSingleton(typeToRegister.GetInterfaces().First(), typeToRegister);
                    break;
                case RegistrationType.Scoped:
                    if (registrationType.AsSelf)
                        Services.AddScoped(typeToRegister);
                    else
                        Services.AddScoped(typeToRegister.GetInterfaces().First(), typeToRegister);
                    break;
                case RegistrationType.Transient:
                    if (registrationType.AsSelf)
                        Services.AddTransient(typeToRegister);
                    else
                        Services.AddTransient(typeToRegister.GetInterfaces().First(), typeToRegister);
                    break;
                default:
                    Services.AddScoped(typeToRegister.GetInterfaces().First(), typeToRegister);
                    break;
            }
        }

        public static void InjectRabbitMq(this IServiceCollection collection)
        {
            var assembly = Assembly.Load("HouseHub.Web.UserApi.Infrastructure");
            assembly.GetTypesForPath("HouseHub.Web.UserApi.Infrastructure.Rabbit").Select(t => t.UnderlyingSystemType).ToList().ForEach(RegisterType);
            collection.BuildServiceProvider().GetRequiredService<RabbitWrapper>();
        }

        public static void InjectGraphQl(this IServiceCollection collection)
        {
            InjectForNamespace(collection, "HouseHub.Web.UserApi.Presentation.GraphQl.Schemas");
            InjectForNamespace(collection, "HouseHub.Web.UserApi.Presentation.GraphQl.Queries");
            InjectForNamespace(collection, "HouseHub.Web.UserApi.Presentation.GraphQl.Mutations");

            collection.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            collection.AddScoped<RootSchema>();
            collection.AddScoped<RootMutation>();
            collection.AddScoped<RootQuery>();
            collection.AddScoped<ISchema, RootSchema>();
            collection.AddScoped<GuidGraphType>();
            collection.AddScoped<UriGraphType>();
            collection.AddScoped<EnumerationGraphType<OrderDirection>>();

            var coreAssembly = Assembly.Load("HouseHub.Web.UserApi.Core");

            coreAssembly.GetTypesForPath("HouseHub.Web.UserApi.Core.GraphQl.Helpers").ForEach(p =>
            {
                RuntimeHelpers.RunClassConstructor(p.TypeHandle);
                collection.AddScoped(p.UnderlyingSystemType);
            });

            coreAssembly.GetTypesForPath("HouseHub.Web.UserApi.Core.GraphQl.InputObjects").ForEach(p => { collection.AddScoped(p.UnderlyingSystemType); });

            coreAssembly.GetTypesForPath("HouseHub.Web.UserApi.Core.GraphQl.OutputObjects").ForEach(p => { collection.AddScoped(p.UnderlyingSystemType); });

            var enumGraphType = typeof(EnumerationGraphType<>);
            coreAssembly.GetEnumsForPath("HouseHub.Web.UserApi.Core.Enums").ForEach(p =>
            {
                collection.AddSingleton(enumGraphType.MakeGenericType(p));
                GraphTypeTypeRegistry.Register(p, enumGraphType.MakeGenericType(p));
                collection.AddScoped(enumGraphType.MakeGenericType(p));
            });

            GraphTypeTypeRegistry.Register(typeof(OrderDirection), enumGraphType.MakeGenericType(typeof(OrderDirection)));
            GraphTypeTypeRegistry.Register(typeof(Guid), typeof(GuidGraphType));

            collection.AddGraphQL(opt => { opt.ExposeExceptions = true; })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddDataLoader();


            ValueConverter.Register(
                typeof(double),
                typeof(float),
                value => Convert.ToSingle(value, NumberFormatInfo.InvariantInfo));

            ValueConverter.Register(
                typeof(float),
                typeof(double),
                value => Convert.ToDouble(value, NumberFormatInfo.InvariantInfo));
        }
    }
}