using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ManagementService.Model.ViewModel
{

    public class NewUserViewModel
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string NationalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Rpassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public long OrgId { get; set; }
    }
    public class UserViewModel
    {

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string OrgName { get; set; }
        public string Email { get; set; }
        public long OrgId { get; set; }

        public string NationalCode { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string ImageLink { get; set; }

        [IgnoreDataMember]
        public string btn { get; set; }

    }
    public class editprofile
    {

        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }


    }
}
