using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementService.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManagementService.Web.Controllers
{
    [Route("api/BaseData")]
    [AutoValidateAntiforgeryToken]
    public class BaseDataController : Controller
    {
        public BaseDataController()
        {
          
        }
        
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        public ViewModels.InitDataViewModel Get()
        {
            var menus = new List<ViewModels.MenuRoutes>() { new ViewModels.MenuRoutes() { RouteText = "خانه" , RouteUrl = "" } };
            var messages = new List<ViewModels.Messages>() { new ViewModels.Messages() { MessageId = 1 , MessageText = ":)" } };
            var user = new ViewModels.UserViewMolel() { UserName = "" };
            return new ViewModels.InitDataViewModel()
            {
                MenuRoutes = menus,
                messages = messages,
                User = user
            };
        }

    }
}
