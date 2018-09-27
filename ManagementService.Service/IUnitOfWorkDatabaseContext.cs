using System;
using System.Collections.Generic;
using System.Text;
using URF.Core.Abstractions;

namespace ManagementService.Service
{
    public interface IUnitOfWorkDatabaseContext:IUnitOfWork
    {
        void SaveChanges();
    }
}
