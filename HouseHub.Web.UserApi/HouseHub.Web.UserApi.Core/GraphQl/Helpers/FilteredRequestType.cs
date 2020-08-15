using GraphQL.Types;
using HouseHub.Web.UserApi.Core.Models.Filters.Interfaces;
using HouseHub.Web.UserApi.Core.Models.Interfaces.Commons;

namespace HouseHub.Web.UserApi.Core.GraphQl.Helpers
{
    public class FilteredRequestType<T> : InputObjectGraphType<IFilter<T>> where T : IIdentifier
    {
        public new const string Description = "Filter used to refine the search response";

        public FilteredRequestType()
        {
            Field(x => x.SearchTerm, true).Description($"The search term which will be used to filter the list of {typeof(T).Name}s");
        }
    }
}