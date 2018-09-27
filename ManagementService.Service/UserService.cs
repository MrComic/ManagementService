using DataTables.Queryable;
using ManagementService.Model.DbSets.Menu;
using ManagementService.Model.DbSets.Roles;
using ManagementService.Model.DbSets.User;
using ManagementService.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
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
        private Data.IRepositoryX<MenuAccess> _menuaccessRepo { get; set; }
        private Data.IRepositoryX<Menu> _menuRepo { get; set; }
        private IUnitOfWorkDatabaseContext unit { get; set; }
        public UserService(Data.IRepositoryX<User> repository,
            Data.IRepositoryX<UsersInRole> RoleRepository,
                Data.IRepositoryX<MenuAccess> _menuaccessRepo,
                Data.IRepositoryX<Menu> menuRepo,
             RoleManager<Role> roleManager,
            IUnitOfWorkDatabaseContext unit, SignInManager<User> signInManager, UserManager<User> userManager) : base(repository)
        {
            this.unit = unit;
            _menuRepo = menuRepo;
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
                    var resDelete = await userManager.DeleteAsync(deleteuser);

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
            return Repository.Queryable().Select(p => new UserViewModel()
            {
                Email = p.Email,
                FirstName = p.Firstname,
                ImageLink = p.ImageLink,
                LastName = p.LastName,
                OrgId = p.OrgId,
                OrgName = "",
                UserId = p.Id,
                UserName = p.UserName
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
    }
}
