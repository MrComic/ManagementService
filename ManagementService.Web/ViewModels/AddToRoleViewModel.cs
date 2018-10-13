using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementService.Web.ViewModels
{
    public class AddToRoleViewModel
    {
        public string rolename { get; set; }
        public Guid userid { get; set; }
    }

    public class RemoveFromViewModel
    {
        public Guid roleid { get; set; }

        public Guid userid { get; set; }
    }
}
