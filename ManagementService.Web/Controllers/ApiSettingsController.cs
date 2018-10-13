using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ManagementService.Service;
using ManagementService.Service.OrgServices;
using ManagementService.Web.ViewModels;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManagementService.Web.Controllers
{
    [Route("api/[controller]")]
    [AutoValidateAntiforgeryToken]
    public class ApiSettingsController : Controller
    {
        private readonly ILogger<ApiSettingsController> _logger;
        private readonly IOptionsSnapshot<ApiSettings> _apiSettingsConfig;
        IUserService userService { get; set; }
        private IAntiforgery antiforgery { get; set; }

        private readonly IAntiForgeryCookieService _antiforgery;
        private readonly ITokenStoreService _tokenService;
        private readonly IOrgService _orgservice;
        public ApiSettingsController(
         //   ILogger<ApiSettingsController> logger,
            IOptionsSnapshot<ApiSettings> apiSettingsConfig, IUserService userService, IAntiForgeryCookieService antiforgery, ITokenStoreService tokenService,
            IAntiforgery antiforgeryService)
        {
           // _logger = logger;
            this.userService = userService;
            _apiSettingsConfig = apiSettingsConfig;

            this.antiforgery = antiforgeryService;
            this._tokenService = tokenService;
            this._antiforgery = antiforgery;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(string t="")
        {
          // _logger.LogError("Error Log");
           // throw new Exception("error");
            // await userService.RegisterTestUser();
            string xsrfToken = "";
            string accessToken = "";
            if (User.Identity.IsAuthenticated)
            {
                var appUser = await userService.FindById(User.Identity.Name);
                var roles = await userService.GetUserRoles(appUser);
                var token = await _tokenService.createAccessTokenAsync(appUser, roles.ToList());
                accessToken =  token.AccessToken;
                xsrfToken = _antiforgery.RegenerateAntiForgeryCookies(new ClaimsPrincipal(new ClaimsIdentity(token.claims, JwtBearerDefaults.AuthenticationScheme)));
            }
            return Json(new { Config = _apiSettingsConfig.Value, Xsrftoken = xsrfToken, AccessToken = accessToken }); 
        }
    }
}
