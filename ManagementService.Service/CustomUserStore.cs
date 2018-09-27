using ManagementService.Data;
using ManagementService.Model.DbSets.Roles;
using ManagementService.Model.DbSets.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Service
{
    public class CustomUserStore:UserStore<User,Role,DatabaseContext,Guid,UserClaim,UsersInRole,UserLogin,UserToken,RoleClaim>
    {
        public CustomUserStore(DatabaseContext database) : base(database)
        {
        }
    }

    public class CustomRoleStore : RoleStore<Role, DatabaseContext, Guid, UsersInRole, RoleClaim>
    {
        public CustomRoleStore(DatabaseContext database) : base(database)
        {
        }
    }
}
