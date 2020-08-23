using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HouseHub.Web.UserApi.Core.Extensions;
using HouseHub.Web.UserApi.Core.Models.Interfaces.Commons;

namespace HouseHub.Web.UserApi.Core.Models.Entities
{
    public class Domain : IIdentifier, IValidEntity
    {
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(128)")] public string Name { get; set; }
        public int YearsOfExperience { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (!Name.HasOnlyLetters())
                errors.Add("The domain Name must contain only letters");
            if (YearsOfExperience <= 0)
                errors.Add("The years of experience for the domain must be > 0");
            return errors;
        }
    }
}