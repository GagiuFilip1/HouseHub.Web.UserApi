using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using HouseHub.Web.UserApi.Core.Extensions;
using HouseHub.Web.UserApi.Core.Models.Interfaces.Commons;

namespace HouseHub.Web.UserApi.Core.Models.Entities
{
    public class User : IValidEntityAsync, IIdentifier
    {
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(32)")] public string FirstName { get; set; }
        [Column(TypeName = "varchar(64)")] public string SecondName { get; set; }
        [Column(TypeName = "varchar(10)")] public string PhoneNumber { get; set; }
        [Column(TypeName = "varchar(128)")] public string Email { get; set; }

        public DateTime RegisteredAt { get; set; }
        [Column(TypeName = "varchar(2048)")] public Uri ProfileUrl { get; set; }

        public async Task<List<string>> ValidateAsync()
        {
            var errors = new List<string>();
            if (!Email.IsValidEmailFormat())
                errors.Add("The email is noi valid");
            if (!await ProfileUrl.IsValidImageAsync())
                errors.Add("The provided image url for profile is not valid");
            if (!PhoneNumber.HasOnlyNumbers())
                errors.Add("The phone number must contain only numbers");
            if (!FirstName.HasOnlyLetters())
                errors.Add("The name of the User must contain only letters");
            if (!SecondName.HasOnlyLetters())
                errors.Add("The name of the User must contain only letters");

            return errors;
        }
    }
}