using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Trackable;

namespace ManagementService.Data
{
    public interface IRepositoryX<TEntity> : ITrackableRepository<TEntity> where TEntity : class, ITrackable
    {
       
    }
}
