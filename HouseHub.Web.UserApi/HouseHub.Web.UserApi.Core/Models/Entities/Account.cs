using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using HouseHub.Web.UserApi.Core.Enums;
using HouseHub.Web.UserApi.Core.Models.Interfaces.Commons;

namespace HouseHub.Web.UserApi.Core.Models.Entities
{
    public abstract class Account : IValidEntity, IIdentifier
    {
        public Guid Id { get; set; }
        public AccountType Type { get; set; }
        [Column(TypeName = "smallint unsigned")] public float Rating { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public List<string> Validate()
        {
            var errors = new List<string>();
            if (Rating < 0 || Rating > 5)
                errors.Add("A user can t have a rating < 0 or > then 5");
            return errors;
        }
    }
}