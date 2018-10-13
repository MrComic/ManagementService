using ManagementService.Model.DbSets.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TrackableEntities.Common.Core;

namespace ManagementService.Model.DbSets.Roles
{
    public class Role:IdentityRole<Guid>, ITrackable, IMergeable
    {
        public virtual List<Menu.MenuAccess> MenuAccesses { get; set; }
        [NotMapped]
        public TrackingState TrackingState { get; set; }
        [NotMapped]
        public ICollection<string> ModifiedProperties { get; set; }
        [NotMapped]
        public Guid EntityIdentifier { get; set; }

        public List<UsersInRole> UsedRoles { get; set; }

    }
}
