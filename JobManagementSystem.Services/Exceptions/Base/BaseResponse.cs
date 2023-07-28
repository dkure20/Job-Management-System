using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Services.Exceptions.Base
{
    public class BaseResponse : Exception
    {
        public int codes { get; set; }
        public BaseResponse(string message, int statusCode) : base(message)
        {
            codes = statusCode;
        }

    }
}
