using ManagementService.Model.DbSets.Menu;
using ManagementService.Model.DbSets.Roles;
using ManagementService.Model.DbSets.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ManagementService.Model.DbSets.Orgs;

namespace ManagementService.Service
{

    public interface IDataBaseSeed{
        Task SeedDatabase();
    }

    public class DataBaseSeed: IDataBaseSeed
    {
        private SignInManager<User> signInManager { get; set; }
        private RoleManager<Role> roleManager { get; set; }
        private UserManager<User> userManager { get; set; }
        private Data.IRepositoryX<UsersInRole> _usersInRolesRepo { get; set; }
        private Data.IRepositoryX<Role> Roles { get; set; }
        private Data.IRepositoryX<MenuAccess> _menuaccessRepo { get; set; }
        private Data.IRepositoryX<Menu> _menuRepo { get; set; }

        private Data.IRepositoryX<Orgs> _OrgRrepo { get; set; }
        private IUnitOfWorkDatabaseContext unit { get; set; }
        public DataBaseSeed(Data.IRepositoryX<User> repository,
            Data.IRepositoryX<UsersInRole> RoleRepository,
                Data.IRepositoryX<MenuAccess> _menuaccessRepo,
                Data.IRepositoryX<Menu> menuRepo,
                Data.IRepositoryX<Role> _Roles,
                Data.IRepositoryX<Orgs> _OrgRrepo,
             RoleManager<Role> roleManager,
            IUnitOfWorkDatabaseContext unit, SignInManager<User> signInManager, UserManager<User> userManager) 
        {
            this.unit = unit;
            _menuRepo = menuRepo;
            this._OrgRrepo = _OrgRrepo;
            Roles = _Roles;
            this._menuaccessRepo = _menuaccessRepo;
            _usersInRolesRepo = RoleRepository;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.userManager = userManager;

            this.signInManager.UserManager = this.userManager;
        }

        public async Task SeedDatabase() {
            try
            {
                var deleteuser = await userManager.FindByNameAsync("administrator");
                if (deleteuser != null)
                {
                    return;
                }
                var role = new Role() { Id = Guid.NewGuid(), Name = "Administrators" };
                var res = await roleManager.CreateAsync(role);
                long OrgId = 0;

                if (_OrgRrepo.Queryable().Count() > 0)
                {
                    OrgId = _OrgRrepo.Queryable().FirstOrDefault().Id;
                }
                else
                {
                    var org = new Orgs() { Name = "شهر سیستم", ParentId = -1 };
                     _OrgRrepo.Insert(org);
                    OrgId = org.Id;
                }

                var user = new User
                {
                    UserName = "administrator",
                    Email = "info@Admin.com",
                    Firstname = "مدیر",
                    LastName = "مدیری",
                    OrgId = OrgId,
                    MobileNumber = "00000000000",
                    NationalCode="0000000000",
                    EmailConfirmed = true,
                    ImageLink = "Admin.png"
                };
                var result = await userManager.CreateAsync(user, "PPpp@123456");

                var createrole = await userManager.AddToRoleAsync(user, role.Name);
                if (result == IdentityResult.Success)
                {
                    if (_menuRepo.Queryable().Count() == 0)
                    {

                        List<Menu> menus = new List<Menu>();
                        menus.Add(new Menu() { IsVisible = true, MenuIconName = "fa fa-home", Name = "خانه", Route = "" });
                        menus.Add(
                            new Menu()
                            {
                                IsVisible = true,
                                MenuIconName = "fa fa-users"
                           ,
                                Route = "/",
                                ParentId = -1,
                                Name = "کاربران"
                            });
                        menus.Add(
                            new Menu()
                            {
                                IsVisible = true,
                                MenuIconName = "fa fa-user",
                                Route = "/users/list",
                                ParentId = 3,
                                Name = "لیست کاربران"
                            });

                        menus.Add(
                            new Menu()
                            {
                                IsVisible = true,
                                MenuIconName = "fa fa-mask",
                                Route = "/role/createrole",
                                ParentId = 3,
                                Name = "مدیریت نقش ها"
                            });

                        foreach (var item in menus)
                        {
                            _menuRepo.Insert(item);
                            _menuaccessRepo.Insert(new MenuAccess() { MenuId = item.Id, RoleId = role.Id });
                        }

                    }
               
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
    }
}
