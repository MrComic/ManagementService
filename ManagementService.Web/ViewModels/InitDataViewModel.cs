using ManagementService.Model.DbSets.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementService.Web.ViewModels
{
    public class InitDataViewModel
    {
      public List<Messages>  messages;
      public List<MenuRoutes> MenuRoutes;
      public UserViewMolel User; 
    }

    public class Messages {
        public long MessageId { get; set; }

        public string MessageText { get; set; }
    }

    public class MenuRoutes {
        public string RouteUrl { get; set; }
        public string RouteText { get; set; }
    }

    public class UserViewMolel {
       public Guid UserId;
        public string  UserName;
        public string NationalCode { get; set; }
        public List<string> Roles { get; set; }
        public List<Menu> Menus { get; set; }
        public long OrgId { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string ImageLink { get; set; }
        public string Email { get; set; }
        public string Access_token { get; set; }
        public string XsrfToken { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RolesViewModel
    {
       public string  RoleId;
        public string Name;
    }

}
