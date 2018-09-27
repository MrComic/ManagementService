using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ManagementService.Model.DbSets.Orgs;
using ManagementService.Model.ViewModel.Orgs;

namespace ManagementService.Service.OrgServices.Mapping
{
    public partial class Mapping :Profile
    {
        public Mapping() {
                CreateMap<Orgs, OrgsViewModel>();
                CreateMap<OrgsViewModel,Orgs>();
        }
    }
}
