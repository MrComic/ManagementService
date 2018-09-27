using ManagementService.Model.DbSets.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TrackableEntities.Common.Core;

namespace ManagementService.Model.DbSets.Menu
{
    public class Menu : ITrackable, IMergeable
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Route { get; set; }

        public bool IsVisible { get; set; }

        public virtual List<MenuAccess> MenuAccesses { get; set; }

        public string MenuIconName { get; set; }

        public long ParentId { get; set; }

        [NotMapped]
        public TrackingState TrackingState { get; set; }
        [NotMapped]
        public ICollection<string> ModifiedProperties { get; set; }
        [NotMapped]
        public Guid EntityIdentifier { get; set; }

    }
}
