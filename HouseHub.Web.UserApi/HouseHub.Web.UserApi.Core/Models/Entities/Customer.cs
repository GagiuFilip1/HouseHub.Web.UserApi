using System;
using System.Collections.Generic;

namespace HouseHub.Web.UserApi.Core.Models.Entities
{
    public class Customer : Account
    {
        public Guid LocationId { get; set; }

        public List<Location> Locations { get; set; }
    }
}