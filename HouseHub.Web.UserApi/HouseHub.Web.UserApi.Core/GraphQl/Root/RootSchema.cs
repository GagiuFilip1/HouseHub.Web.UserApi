﻿using GraphQL;
using GraphQL.Types;

namespace HouseHub.Web.UserApi.Core.GraphQl.Root
{
    public class RootSchema : Schema
    {
        public RootSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
            Mutation = resolver.Resolve<RootMutation>();
        }
    }
}
