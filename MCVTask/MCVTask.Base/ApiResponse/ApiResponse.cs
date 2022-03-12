using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCVTask.Base.ApiResponse
{
    public class ApiResponse<T>
    {
        public T ResponseData { get; set; }
        public string CommandMessage { get; set; }
        public ApiResponse()
        {
            ResponseData = default(T);
            CommandMessage = null;
        }
    }
}
