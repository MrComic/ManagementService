using DataTables.Queryable;
using ManagementService.Model.DbSets.Menu;
using ManagementService.Model.DbSets.User;
using ManagementService.Model.DbSets.User.ViewModels;
using ManagementService.Model.ViewModel;
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
    }
}