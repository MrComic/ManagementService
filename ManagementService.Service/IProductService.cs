using DataTables.Queryable;
using ManagementService.Model.DbSets.Menu;
using ManagementService.Model.DbSets.Roles;
using ManagementService.Model.DbSets.User;
using ManagementService.Model.DbSets.User.ViewModels;
using ManagementService.Model.ViewModel;
using ManagementService.Model.ViewModel.UsersInRoles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using URF.Core.Abstractions.Services;

namespace ManagementService.Service
{
    public interface IUserService : IService<User>
    {
        Task<SignInResult> Authenticate(string username, string password,bool rememberMe);
        Task<User> FindById(string username);
        Task RegisterTestUser();
        User FindByUserId(Guid userid);

        Task<ClaimsPrincipal> CreateClaim(User user);
        Task<IList<string>> GetUserRoles(User user);
        Task Logout();
        Task<List<Menu>> GetMenus(User user);
        IPagedList<UserViewModel> GetUsers(DataTables.Queryable.DataTablesRequest<UserViewModel> request);
        Task<bool> IsDuplicateNationalCode(string nationalCode);
        Task<IdentityResult> NewUser(NewUserViewModel model);
        Task<bool> UnlockUser(Guid userId);
        Task<bool> EditProfile(Model.ViewModel.UserProfile.editprofileModel temp);
        List<UsersInRoles> GetUserCompleteRoles(Guid userid);
        List<Role> GetRoles1();
        Task<IdentityResult> AddToRole(Guid userid,string RoleName);
        Task<bool> IsInRole(Guid userid, string rolename);
        IPagedList<RoleViewModel> GetRoles(DataTablesRequest<RoleViewModel> request);
        Task<IdentityResult> RemoveUserFromRole(Guid userid, Guid roleid);
    }
}