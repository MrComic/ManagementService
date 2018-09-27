using ManagementService.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Service
{
    public class UnitOfWorkDatabaseContext:URF.Core.EF.UnitOfWork,IUnitOfWorkDatabaseContext
    {
        public UnitOfWorkDatabaseContext(DatabaseContext dbcontext) : base(dbcontext)
        {

        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
