using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Model.DbSets.User.ViewModels
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        //public string Password { get; set; }
        public string access_token { get; set; }

        public string NationalCode { get; set; }

        public string refresh_token { get; set; }
        public string xsrfToken { get; set; }
    }
}
