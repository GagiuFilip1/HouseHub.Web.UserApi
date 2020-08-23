using System;

namespace HouseHub.Web.UserApi.Core.Models.Entities
{
    public class Customer : Account
    {
        public Guid LocationId { get; set; }

        public Location Location { get; set; }
    }
}