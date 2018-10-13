using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Model.ViewModel.UsersInRoles
{
    public class UsersInRoles
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        
        public string RoleName { get; set; }
    }
}
