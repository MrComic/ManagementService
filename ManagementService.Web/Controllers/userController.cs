using DataTables.Queryable;
using ManagementService.Model.ViewModel;
using ManagementService.Service;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ManagementService.Web.Controllers
{
   // [AutoValidateAntiforgeryToken]
    [Route("/api/user/[action]")]
    public class userController : BaseController
    {
        private IUserService _userService { get; set; }
        private readonly IConfiguration _configuration;
        private IAntiforgery antiforgery { get; set; }

        private readonly IAntiForgeryCookieService _antiforgery;
        private readonly ITokenStoreService _tokenService;
        private IHostingEnvironment server;
        public userController(IHostingEnvironment environment, IUserService userService)
        {
            _userService = userService;
            server = environment;
        }


        [Authorize]
        [HttpGet]
        public JsonResult list()
        {

            var request = new DataTablesRequest<Model.ViewModel.UserViewModel>(Request?.QueryString.Value);
            IPagedList<Model.ViewModel.UserViewModel> model = _userService.GetUsers(request);
            return JsonDataTable(model, request.Draw); //new Model.ViewModel.DataTabaleAjaxViewModel<List<Model.ViewModel.UserViewModel>>() { Data = model };
        }

        [Authorize]
        [HttpGet]
        public FileContentResult pic(string id)
        {

            var path = server.WebRootPath + "/UserPics/" + id;
            var generalPic = server.WebRootPath + "/UserPics/" + "user.png";
            if (!System.IO.File.Exists(path))
            {
                var extention = System.IO.Path.GetExtension(generalPic).Replace(",", "");
                Byte[] b = System.IO.File.ReadAllBytes(generalPic);
                return File(b, "image/" + extention);

            }
            else
            {
                var extention = System.IO.Path.GetExtension(path).Replace(",", "");
                Byte[] b = System.IO.File.ReadAllBytes(path);
                return File(b, "image/" + extention);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<HttpResult> NewUser([FromBody]NewUserViewModel Model)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    if (!await _userService.IsDuplicateNationalCode(Model.NationalCode))
                    {
                        /*task:Add Image Upload*/
                        IdentityResult res = await _userService.NewUser(Model);
                        if (res.Succeeded)
                        {
                            return new HttpResult()
                            {
                                Success = true,
                                Message = "با موفقیت ایجاد شد"

                            };
                        }
                        else
                        {
                            return new HttpResult()
                            {
                                Success = false,
                                Message = string.Join(",", res.Errors)
                            };
                        }
                    }
                    else
                    {
                        return new HttpResult()
                        {
                            Success = false,
                            Message = "کد ملی تکراری"

                        };
                    }
                }
                else
                {
                    return new HttpResult()
                    {
                        Success = false,
                        Message = "درخواست نامعتبر"

                    };
                }
            }
            catch
            {
                return new HttpResult()
                {
                    Success = false,
                    Message = "مشکلی در انجام عملیات جاری وجود دارد"
                };
            }
        }





    }
}
