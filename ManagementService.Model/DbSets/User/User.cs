using ManagementService.Model.DbSets.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TrackableEntities.Common.Core;
using URF.Core.EF.Trackable;

namespace ManagementService.Model.DbSets.User
{
    public class User : IdentityUser<Guid>,ITrackable, IMergeable
    {
        [NotMapped]
        public TrackingState TrackingState { get; set; }
        [NotMapped]
        public ICollection<string> ModifiedProperties { get; set; }
        [NotMapped]
        public Guid EntityIdentifier { get; set; }

        public string Firstname { get; set; }

        public string LastName { get; set; }

        public string ImageLink { get; set; }
        
        public string NationalCode { get; set; }

        public long OrgId { get; set; }
    }
}
