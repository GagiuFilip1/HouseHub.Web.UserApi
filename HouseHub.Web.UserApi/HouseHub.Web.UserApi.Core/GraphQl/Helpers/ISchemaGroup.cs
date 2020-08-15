using HouseHub.Web.UserApi.Core.GraphQl.Root;

namespace HouseHub.Web.UserApi.Core.GraphQl.Helpers
{
    public interface ISchemaGroup
    {
        void SetGroup(RootQuery query);
        void SetGroup(RootMutation mutation);
    }
}