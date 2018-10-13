using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Model.Exceptions
{
    public class UserNotFoundException:Exception
    {
        public UserNotFoundException() : 
            base("کاربر مورد نظر یافت نشد"){

        }
    }
}
