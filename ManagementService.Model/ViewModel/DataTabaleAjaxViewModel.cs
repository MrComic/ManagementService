using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Model.ViewModel
{
    public class DataTabaleAjaxViewModel<T> where T : class
    {
        public T Data { get; set; }
    }
}
