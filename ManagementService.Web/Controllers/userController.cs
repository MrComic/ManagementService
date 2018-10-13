using DataTables.Queryable;
using ManagementService.Model.Exceptions;
using ManagementService.Model.ViewModel;
using ManagementService.Model.ViewModel.UsersInRoles;
using ManagementService.Service;
using ManagementService.Web.ViewModels;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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


        /// <summary>
        ///  باز کردن قفل کاربر پس از اشتباه زدن چند بار رمز عبور
        /// </summary>
        /// <param name="userid">شناسه کاربری</param>
        /// <returns>HttpResult</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrators")]
        [HttpPost]
        public async Task<JsonResult> UnlockUser([FromBody]Guid userid = new Guid())
        {
            try
            {
                if (userid == Guid.Empty)
                {
                    return Json(new HttpResult()
                    {
                        Success = false,
                        Message = "درخواست نامعتبر",
                        Data = null
                    });
                }
                else
                {
                    bool Res = await _userService.UnlockUser(userid);
                    if (Res)
                    {
                        return Json(new HttpResult()
                        {
                            Success = true,
                            Message = "با موفقیت انجام شد",
                            Data = null
                        });
                    }
                    else
                    {
                        return Json(new HttpResult()
                        {
                            Success = false,
                            Message = "مشکلی در انجام عملیات جاری وجود دارد",
                            Data = null
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "UserNotFound")
                {
                    return Json(new HttpResult()
                    {
                        Success = true,
                        Message = "با موفقیت انجام شد",
                        Data = null
                    });
                }
                else
                {
                    return Json(new HttpResult()
                    {
                        Success = false,
                        Message = "مشکلی در انجام عملیات جاری وجود دارد",
                        Data = null
                    });

                }
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<JsonResult> EditRole([FromBody]Guid RoleId = new Guid())
        {
            return null;
        }

        [Authorize(Roles = "Administrators")]
        //[Authorize()]
        [HttpGet]
        public JsonResult list()
        {
            var request = new DataTablesRequest<Model.ViewModel.UserViewModel>(Request?.QueryString.Value);
            IPagedList<Model.ViewModel.UserViewModel> model = _userService.GetUsers(request);
            return JsonDataTable(model, request.Draw); //new Model.ViewModel.DataTabaleAjaxViewModel<List<Model.ViewModel.UserViewModel>>() { Data = model };
        }
        [Authorize]
        [HttpGet]
        public JsonResult RoleList()
        {
            var request = new DataTablesRequest<ManagementService.Model.ViewModel.RoleViewModel>(Request?.QueryString.Value);
            IPagedList<ManagementService.Model.ViewModel.RoleViewModel> model = _userService.GetRoles(request);
            return JsonDataTable(model, request.Draw);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrators")]
        [HttpPost]
        public async Task<HttpResult> RemoveUserFromRole(RemoveFromViewModel model)
        {
            try
            {
                if (model.userid == Guid.Empty || model.roleid == Guid.Empty)
                {
                    return new HttpResult()
                    {
                        Success = false,
                        Message = "درخواست نامعتبر",
                        Data = null
                    };
                }
                else
                {
                    IdentityResult res = await _userService.RemoveUserFromRole(model.userid, model.roleid);
                    if (res.Succeeded)
                    {
                        return new HttpResult()
                        {
                            Success = true,
                            Message = "با موفقیت انجام شد",
                            Data = null
                        };
                    }
                    else
                    {
                        return new HttpResult()
                        {
                            Success = false,
                            Message = "مشکلی در انجام عملیات جاری وجود دارد",
                            Data = null
                        };
                    }
                }
            }
            catch (Exception ex) when (ex is UserNotFoundException || ex is RoleNotFoundException)
            {
                {
                    return new HttpResult()
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    };
                }
            }
            catch
            {
                return new HttpResult()
                {
                    Success = false,
                    Message = "مشکلی در انجام عملیات جاری وجود دارد",
                    Data = null
                };
            }

        }

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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrators")]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrators")]
        [HttpGet]
        public ActionResult ListUserRoles(Guid userid = new Guid())
        {
            if (userid == Guid.Empty)
            {
                return Json(new List<UsersInRoles>());
            }
            else
            {
                return Json(_userService.GetUserCompleteRoles(userid));
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrators")]
        [HttpGet]
        public ActionResult Roles()
        {
            return Json(_userService.GetRoles1());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrators")]
        [HttpPost]
        public async Task<HttpResult> AddToRole(AddToRoleViewModel model)
        {
            if (string.IsNullOrEmpty(model.rolename) || model.userid == Guid.Empty)
            {
                return new HttpResult() { Success = false, Message = "درخواست نامعتبر" };
            }

            else
            {
                if (!await _userService.IsInRole(model.userid, model.rolename))
                {
                    var identityres = await _userService.AddToRole(model.userid, model.rolename);
                    if (identityres.Succeeded)
                    {
                        return new HttpResult() { Success = true, Message = "با موفقیت انجام شد" };
                    }
                    else
                    {
                        return new HttpResult() { Success = true, Message = string.Join(",", identityres.Errors), Data = null };
                    }
                }
                else
                {
                    return new HttpResult() { Success = false, Message = "کاربر این نقش را دارا می‌باشد" };
                }
            }
        }

    }
}
