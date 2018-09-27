using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ManagementService.Model.DbSets.User;
using ManagementService.Service;
using ManagementService.Web.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManagementService.Web.Controllers
{
    [Route("/api/auth/[action]")]
    [AutoValidateAntiforgeryToken]
    public class AuthController : Controller
    {
        private IUserService _userService { get; set; }
        private readonly IConfiguration _configuration;
        private IAntiforgery antiforgery { get; set; }

        private readonly IAntiForgeryCookieService _antiforgery;
        private readonly ITokenStoreService _tokenService;
        public AuthController(IUserService userService, IConfiguration configuration, IAntiForgeryCookieService antiforgery,ITokenStoreService tokenService, IAntiforgery antiforgeryService)
        {
            this.antiforgery = antiforgeryService;
            this._tokenService = tokenService; 
            this._configuration = configuration;
            this._userService = userService;
            this._antiforgery = antiforgery;
            
        }

        private string GenerateJwtToken(string email, User user, List<Claim> claims)
        {
            //var claims = new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, email),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            //};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost]
       
        // [IgnoreAntiforgeryToken]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpResult> GetUserInfo() {
           // await antiforgery.ValidateRequestAsync(HttpContext);

            var id = await _userService.FindById(HttpContext.User.Identity.Name);

            var Menus = (await _userService.GetMenus(id)).ToList();
            if (!Menus.Any())
            {
                Menus.Add(new Model.DbSets.Menu.Menu()
                {
                    Id = 1,
                    Name = "خانه",
                    Route = "",
                    IsVisible = true
                });
            }
            var roles = await _userService.GetUserRoles(id);
            if (id != null)
            {
                return new HttpResult() { Success = true, Message = "ok",
                    Data = 
                    new UserViewMolel() {
                        Menus = Menus,
                        UserName = id.UserName,
                        OrgId = 1,
                        UserId = id.Id,
                        NationalCode = id.NationalCode,
                        Email = id.Email,
                        Firstname = id.Firstname,
                        ImageLink = id.ImageLink,
                        LastName = id.LastName,
                        PhoneNumber=id.PhoneNumber,
                        Roles = roles.ToList()
                    } };
            }
            else
            {
                return new HttpResult() { Success = false, Message = "کاربر مورد نظر پیدا نشد" };
            }
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpResult> editprofile([FromBody]Model.ViewModel.editprofile editprofile)
        {
            // await antiforgery.ValidateRequestAsync(HttpContext);
            
            return null;
        }
        [HttpGet]
        public async Task<Model.ViewModel.editprofile> GetUserId(string userid)
        {
            // await antiforgery.ValidateRequestAsync(HttpContext);
            var id = _userService.FindByUserId(new Guid(userid));
            Model.ViewModel.editprofile temp = new Model.ViewModel.editprofile();
            temp.UserId = id.Id;
            temp.Firstname = id.Firstname;
            temp.LastName = id.Firstname;
            temp.NationalCode ="";
            temp.PhoneNumber = id.PhoneNumber;
            temp.MobileNumber = "";
            temp.Email = id.Email;
            return temp;
        }





        public async Task<IActionResult> logout()
        {
            try
            {
                await _userService.Logout();
                _antiforgery.DeleteAntiForgeryCookies();
                 //HttpContext.Session.Clear();
                return Json(new HttpResult() { Success = true, Data = null, Message = "با موفقیت انجام شد" });
            }
            catch (Exception ex) {
                return Json(new HttpResult() { Success = false, Data = null, Message = "مشکلی در انجام عملیات وجود دارد" });
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> login([FromBody]LoginModel user)
        {
            try
            {
                await _userService.Logout();
                _antiforgery.DeleteAntiForgeryCookies();

                var result = await _userService.Authenticate(user.Username, user.Password, user.RememberMe);
                if (result.Succeeded)
                {
                    var appUser = await _userService.FindById(user.Username);
                    var roles = await _userService.GetUserRoles(appUser);
                    var token = await _tokenService.createAccessTokenAsync(appUser, roles.ToList());
                    var Menus = (await _userService.GetMenus(appUser)).ToList();
                    if (!Menus.Any())
                    {
                        Menus.Add(new Model.DbSets.Menu.Menu()
                        {
                            Id = 1,
                            Name = "خانه",
                            Route = "",
                            IsVisible = true
                        });
                    }

                    string xsrfToken = _antiforgery.RegenerateAntiForgeryCookies(new ClaimsPrincipal(new ClaimsIdentity( token.claims,JwtBearerDefaults.AuthenticationScheme)));
                    return Json(
                        new HttpResult()
                        {
                            Success = true,
                            Message = "",
                            Data = new UserViewMolel()
                            {
                                Menus = Menus,
                                Email = appUser.Email,
                                Roles = roles.ToList(),
                                ImageLink = appUser.ImageLink,
                                OrgId = appUser.OrgId,
                                XsrfToken = xsrfToken,
                                UserId = appUser.Id,
                                NationalCode = appUser.NationalCode,
                                Firstname = appUser.Firstname,
                                LastName = appUser.LastName,
                                UserName = appUser.UserName,
                                PhoneNumber=appUser.PhoneNumber,
                                Access_token = token.AccessToken //GenerateJwtToken(user.username, appUser,claims.Claims.ToList())
                            }
                        });
                }
                else if (result.IsLockedOut)
                {
                    return Json(new HttpResult()
                    {
                        Success = false,
                        Message = "کاربری شما قفل شده"
                    });
                }
                else
                {
                    return Json(new HttpResult()
                    {
                        Success = false,
                        Message = "نام کاربری یا رمز عبور اشتباه است"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new HttpResult()
                {
                    Success = false,
                    Message = "مشکلی در انجام عملیات وجود دارد"
                });
            }
        }
    }
}
