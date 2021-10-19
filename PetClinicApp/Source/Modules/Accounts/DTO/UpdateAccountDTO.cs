using System;
using System.ComponentModel.DataAnnotations;

namespace PetClinicApp.Source.Modules.Accounts.DTO
{
    public class UpdateAccountDTO
    {
        [Required(ErrorMessage = "The name field is required.")]
        [MinLength(3, ErrorMessage = "the name field must contain at least 3 characters")]
        public string FullName { get; set; }
        [EmailAddress(ErrorMessage = "The e-mail is invalid")]
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
