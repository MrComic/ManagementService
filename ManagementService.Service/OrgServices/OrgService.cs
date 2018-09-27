using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ManagementService.Model.DbSets.Orgs;
using ManagementService.Model.ViewModel.Orgs;
using URF.Core.Services;
using System.Linq;
using AutoMapper.QueryableExtensions;
namespace ManagementService.Service.OrgServices
{
    public interface IOrgService {
        List<OrgsViewModel> GetLibOrgs(long ParentOrgId);
    }

    public class OrgService: Service<Orgs>, IOrgService
    {
        private readonly IMapper _mapper;
        Data.IRepositoryX<Orgs> _repository { get; set; }
        public OrgService(Data.IRepositoryX<Orgs> repository, IMapper mapper) : base(repository)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public List<OrgsViewModel> GetLibOrgs(long ParentOrgId)
        {
            return _repository.Queryable().ProjectTo<OrgsViewModel>(_mapper.ConfigurationProvider).ToList();
        }
    }
}
