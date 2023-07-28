using JobManagementSystem.Services.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Services.Exceptions
{
    public class UserDoesNotExist : BaseResponse
    {
        public UserDoesNotExist(string message, int code) : base(message, code)
        {
        }
    }
}
