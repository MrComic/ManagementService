using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TrackableEntities.Common.Core;

namespace ManagementService.Model.DbSets.Menu
{
    public class MenuAccess: ITrackable, IMergeable
    {
        public long MenuId { get; set; }

        public Guid RoleId { get; set; }

        [NotMapped]
        public TrackingState TrackingState { get; set; }
        [NotMapped]
        public ICollection<string> ModifiedProperties { get; set; }
        [NotMapped]
        public Guid EntityIdentifier { get; set; }
    }
}
