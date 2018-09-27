using ManagementService.Model.ViewModel.Orgs;
using ManagementService.Service.OrgServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementService.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Route("/api/org/[action]")]
    public class OrgController:BaseController
    {
        private readonly IOrgService _orgService;
        private readonly IHostingEnvironment environment;
        public OrgController(IHostingEnvironment environment, IOrgService orgService)
        {
            this._orgService = orgService;
            this.environment = environment;
        }

        public List<OrgsViewModel> Get()
        {
            return _orgService.GetLibOrgs(1);
        }
    }
}
