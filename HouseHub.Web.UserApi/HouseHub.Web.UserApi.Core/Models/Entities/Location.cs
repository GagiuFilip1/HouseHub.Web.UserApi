using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HouseHub.Web.UserApi.Core.Extensions;
using HouseHub.Web.UserApi.Core.Models.Interfaces.Commons;

namespace HouseHub.Web.UserApi.Core.Models.Entities
{
    public class Location : IIdentifier, IValidEntity
    {
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(128)")] public string StreetAddress { get; set; }
        [Column(TypeName = "varchar(16)")] public string PostalNumber { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (!PostalNumber.HasOnlyNumbersAndLetters())
                errors.Add("The postal number can contain only numbers and letters");
            return errors;
        }
    }
}