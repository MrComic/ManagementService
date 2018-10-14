using DataTables.Queryable;
using ManagementService.Model.DbSets.Menu;
using ManagementService.Model.DbSets.Roles;
using ManagementService.Model.DbSets.User;
using ManagementService.Model.Exceptions;
using ManagementService.Model.ViewModel;
using ManagementService.Model.ViewModel.UsersInRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using URF.Core.Services;

namespace ManagementService.Service
{
    public class UserService : Service<User>, IUserService
    {
        private SignInManager<User> signInManager { get; set; }
        private RoleManager<Role> roleManager { get; set; }
        private UserManager<User> userManager { get; set; }
        private Data.IRepositoryX<UsersInRole> _usersInRolesRepo { get; set; }
        private Data.IRepositoryX<Role> Roles { get; set; }
        private Data.IRepositoryX<MenuAccess> _menuaccessRepo { get; set; }
        private Data.IRepositoryX<Menu> _menuRepo { get; set; }
        private IUnitOfWorkDatabaseContext unit { get; set; }
        public UserService(Data.IRepositoryX<User> repository,
            Data.IRepositoryX<UsersInRole> RoleRepository,
                Data.IRepositoryX<MenuAccess> _menuaccessRepo,
                Data.IRepositoryX<Menu> menuRepo,
                Data.IRepositoryX<Role> _Roles,
             RoleManager<Role> roleManager,
            IUnitOfWorkDatabaseContext unit, SignInManager<User> signInManager, UserManager<User> userManager) : base(repository)
        {
            this.unit = unit;
            _menuRepo = menuRepo;

            Roles = _Roles;
            this._menuaccessRepo = _menuaccessRepo;
            _usersInRolesRepo = RoleRepository;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.userManager = userManager;

            this.signInManager.UserManager = this.userManager;
        }

        public Task<SignInResult> Authenticate(string username, string password, bool rememberMe)
        {
            return signInManager.PasswordSignInAsync(username, password, rememberMe, true);
        }

        public Task<ClaimsPrincipal> CreateClaim(User user)
        {
            return signInManager.ClaimsFactory.CreateAsync(user);
        }

        public Task<User> FindById(string username)
        {
            return userManager.FindByNameAsync(username);
        }

        public User FindByUserId(Guid userid) => userManager.Users.Where(s => s.Id == userid).FirstOrDefault();



        public async Task RegisterTestUser()
        {
            try
            {
                var deleteuser = await userManager.FindByNameAsync("administrator");
                if (deleteuser != null)
                {
                    PasswordHasher<User> hasher = new PasswordHasher<User>();
                   string hashedPassword = hasher.HashPassword(deleteuser, "PPpp@123456");
                   // var resDelete = await userManager.DeleteAsync(deleteuser);

                }
                var role = new Role() { Id = Guid.NewGuid(), Name = "Administrators" };
                var res = await roleManager.CreateAsync(role);

                var user = new User
                {
                    UserName = "administrator",
                    Email = "Mohammad.farahani70@gmail.com",
                    Firstname = "mohammad",
                    LastName = "farahani",
                    OrgId = 1,
                    ImageLink = "Admin.png",

                };
                var result = await userManager.CreateAsync(user, "PPpp@123456");

                var createrole = await userManager.AddToRoleAsync(user, role.Name);
                if (result == IdentityResult.Success)
                {
                    var menu = new Menu() { IsVisible = true, MenuIconName = "fa fa-home", Name = "خانه", Route = "" };
                    _menuRepo.Insert(menu);
                    unit.SaveChanges();
                    _menuaccessRepo.Insert(new MenuAccess() { MenuId = menu.Id, RoleId = role.Id });
                    unit.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public Task<IList<string>> GetUserRoles(User user)
        {
            return userManager.GetRolesAsync(user);
        }

        public Task Logout()
        {
            return signInManager.SignOutAsync();
        }

        public async Task<List<Menu>> GetMenus(User user)
        {
            var userroles = (await _usersInRolesRepo.Query().Where(p => p.UserId == user.Id).SelectAsync()).Select(l => l.RoleId).ToList();
            List<long> Menus = (await _menuaccessRepo.Query().Where(p => userroles.Contains(p.RoleId)).SelectAsync()).Select(k => k.MenuId).ToList();
            var menuEntity = (await _menuRepo.Query().Where(p => Menus.Contains(p.Id)).SelectAsync()).ToList();
            return menuEntity;
        }



        public IPagedList<UserViewModel> GetUsers(DataTablesRequest<UserViewModel> request)
        {
            return Repository.Queryable().Include(p=>p.Org).Select(p => new UserViewModel()
            {
                Email = p.Email,
                FirstName = p.Firstname,
                ImageLink = p.ImageLink,
                LastName = p.LastName,
                OrgId = p.OrgId,
                OrgName = p.Org.Name,
                UserId = p.Id,
                UserName = p.UserName
            }).ToPagedList(request);
        }
        public IPagedList<RoleViewModel> GetRoles(DataTablesRequest<RoleViewModel> request)
        {
            return Roles.Queryable().Select(p => new RoleViewModel()
            {
                Name = p.Name,
               RoleId=Convert.ToString(p.Id)
            }).ToPagedList(request);

        }

        public Task<bool> IsDuplicateNationalCode(string nationalCode)
        {
            var cts = new CancellationTokenSource();
            return Query().Where(p => p.NationalCode == nationalCode).AnyAsync(cts.Token);
        }

        public Task<IdentityResult> NewUser(NewUserViewModel model)
        {
            /*task:Add Image Upload*/
            return userManager.CreateAsync(new User()
            {
                Email = model.Email,
                EmailConfirmed = true,
                Firstname = model.Firstname,
                ImageLink = "",
                LastName = model.LastName,
                LockoutEnabled = true,
                NationalCode = model.NationalCode,
                UserName = model.NationalCode,
                OrgId = model.OrgId,
                PhoneNumber = model.PhoneNumber,
            }, model.Password);
        }

        public async Task<bool> UnlockUser(Guid userId)
        {
            var user =  this.userManager.Users.Where(p=>p.Id == userId).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("UserNotFound");
            }
            else
            {
                IdentityResult Res1= await userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddMinutes(-1));
                IdentityResult Res2= await this.userManager.ResetAccessFailedCountAsync(user);
                if (Res1.Succeeded && Res2.Succeeded)
                {
                    return true;
                }
                else
                {
                    throw new Exception(String.Join(",",Res1.Errors) + " " + String.Join(",", Res2.Errors));
                }
            }
        }
        public async Task<bool> EditProfile(Model.ViewModel.UserProfile.editprofileModel temp)
        {
            try { 
            var user = userManager.Users.Where(s => s.Id == new Guid(temp.UserId)).FirstOrDefault();
            if (user.UserName.Length > 0)
            {
                user.Firstname = temp.Firstname;
                user.LastName = temp.LastName;
                user.NationalCode = temp.NationalCode;
                user.PhoneNumber = temp.PhoneNumber;
                user.Email = temp.Email;
                user.MobileNumber = temp.MobileNumber;
                await userManager.UpdateAsync(user);
                return true;
            }
            else { return false; }
            }
            catch(Exception e)
            {
                
                return false;
            }
        }

        public List<UsersInRoles> GetUserCompleteRoles(Guid userid)
        {
            return this._usersInRolesRepo.Queryable().Where(p => p.UserId == userid).Include(p => p.Role)
                .Select(p => new UsersInRoles() {
                    UserId = p.UserId,
                    RoleId = p.RoleId,
                    RoleName = p.Role.Name
                }).ToList(); 
        }

        public List<Role> GetRoles1()
        {
            return this.roleManager.Roles.ToList();
        }

        public  Task<IdentityResult> AddToRole(Guid userid, string RoleName)
        {
            var user = userManager.Users.Where(s => s.Id == userid).FirstOrDefault();
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return  this.userManager.AddToRoleAsync(user, RoleName);
        }

        public Task<bool> IsInRole(Guid userid, string rolename)
        {
            var user = userManager.Users.Where(s => s.Id == userid).FirstOrDefault();
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return this.userManager.IsInRoleAsync(user, rolename);
        }

        public  Task<IdentityResult> RemoveUserFromRole(Guid userid, Guid roleid)
        {
            var user = userManager.Users.Where(s => s.Id == userid).FirstOrDefault();
            var role = roleManager.Roles.Where(s => s.Id == roleid).FirstOrDefault();
            if (user == null)
            {
                throw new  UserNotFoundException();
            }
            if (role == null)
            {
                throw new RoleNotFoundException();
            }
            return userManager.RemoveFromRoleAsync(user, role.Name);
        }
    }
}
