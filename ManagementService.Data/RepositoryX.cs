using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TrackableEntities.Common.Core;
using URF.Core.EF.Trackable;

namespace ManagementService.Data
{
    public class RepositoryX<TEntity> : TrackableRepository<TEntity>, IRepositoryX<TEntity> where TEntity : class, ITrackable
    {
        public RepositoryX(DatabaseContext context) : base(context)
        {

        }
    }
}
