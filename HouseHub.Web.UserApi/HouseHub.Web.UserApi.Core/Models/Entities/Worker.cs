using System;
using System.Collections.Generic;

namespace HouseHub.Web.UserApi.Core.Models.Entities
{
    public class Worker : Account
    {
        public int CompletedJobs { get; set; }
        public Guid DomainId { get; set; }

        public Domain Domain { get; set; }

        public new List<string> Validate()
        {
            var errors = base.Validate();
            if (CompletedJobs < 0)
                errors.Add("A worker can t have a negative number of completed jobs");

            return errors;
        }
    }
}