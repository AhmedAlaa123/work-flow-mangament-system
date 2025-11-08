using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Exceptions
{
    public class UserFriendlyException:Exception
    {
        public Dictionary<string,string> Errors { get; set; }
        public UserFriendlyException(Dictionary<string, string> _errors)  
        {
            Errors=_errors;
        }
        public UserFriendlyException()
        {
            Errors=new();
        }
    }
}
