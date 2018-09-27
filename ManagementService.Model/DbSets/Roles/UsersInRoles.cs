using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TrackableEntities.Common.Core;

namespace ManagementService.Model.DbSets.Roles
{
    public class UsersInRole: IdentityUserRole<Guid>,ITrackable
    {
        [NotMapped]
        public TrackingState TrackingState { get; set; }
        [NotMapped]
        public ICollection<string> ModifiedProperties { get; set; }
        [NotMapped]
        public Guid EntityIdentifier { get; set; }

        public long OrgId { get; set; }
        
    }

    public class UserClaim : IdentityUserClaim<Guid>
    {

    }

    public class UserLogin : IdentityUserLogin<Guid>
    {

    }

    public class RoleClaim : IdentityRoleClaim<Guid>
    {

    }

    public class UserToken : IdentityUserToken<Guid>
    {

    }
}
