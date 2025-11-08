using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.ExtensionsMethods
{
    public static class ExceptionExtensions
    {
        public static string GetExceptionMessage(this Exception ex)
        {
            if (ex.InnerException is not null)
            {
                return ex.InnerException.Message;
            }
            return ex.Message;
        }
    }
}
