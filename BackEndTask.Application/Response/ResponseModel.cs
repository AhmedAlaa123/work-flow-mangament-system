using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Response
{
    public class ResponseModel<T> 
    {
        public bool IsSuccess {  get; private set; }
        public string ErrorMessage { get; private set; }
        public T Data { get; set; }
        
        private ResponseModel()
        {

        }


        public static ResponseModel<T> Success(T data)
        {
            return new ResponseModel<T>
            {
                IsSuccess=true,
                Data=data
            };
        }
        public static ResponseModel<T> Fail(T data,string erroMessage)
        {
            return new ResponseModel<T>
            {
                IsSuccess=false,
                Data=data,
                ErrorMessage=erroMessage
            };
        }

    }
}
