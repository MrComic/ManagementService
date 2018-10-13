using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Model.Exceptions
{
    public class RoleNotFoundException:Exception
    {
        public RoleNotFoundException() : base("نقش مورد نظر وجود ندارد") {

        }
    }
}
