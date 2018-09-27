using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Model.ViewModel.Orgs
{
    public class OrgsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
    }
}
