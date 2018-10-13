using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TrackableEntities.Common.Core;

namespace ManagementService.Model.DbSets.Orgs
{
    public class Orgs: ITrackable
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        [NotMapped]
        public TrackingState TrackingState { get; set; }
        [NotMapped]
        public ICollection<string> ModifiedProperties { get; set; }
        public virtual List<User.User> Users { get; set; }
    }
}
