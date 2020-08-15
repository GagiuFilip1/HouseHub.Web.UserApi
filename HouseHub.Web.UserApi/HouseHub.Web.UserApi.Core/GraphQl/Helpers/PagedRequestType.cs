using GraphQL.Types;
using HouseHub.Web.UserApi.Core.Models.Helpers.Commons;

namespace HouseHub.Web.UserApi.Core.GraphQl.Helpers
{
    public class PagedRequestType : InputObjectGraphType<Pagination>
    {
        public new static readonly string Description = "Pagination Settings";

        public PagedRequestType()
        {
            Field(x => x.Take, true).Description("Count of items to return");
            Field(x => x.Offset, true).Description("Count of items to skip");
        }
    }
}