using BackEndTask.Application.Exceptions;
using BackEndTask.Application.ExtensionsMethods;
using BackEndTask.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.MeddilWares
{
    public class ExceptionHandlerMeddilware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlerMeddilware> _logger;
        public ExceptionHandlerMeddilware(RequestDelegate next, ILogger<ExceptionHandlerMeddilware> logger)
        {
            _next=next;
            _logger=logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                var responseCode = ex switch
                {
                    UserFriendlyException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError,
                };

                context.Response.ContentType = "application/json";
                ErrorResponse error;
                if (ex is UserFriendlyException)
                {
                    error=new ErrorResponse
                    {
                        Status=responseCode,
                        Title="Custome Errors",
                        Errors=(ex as UserFriendlyException).Errors
                    };
                }
                else
                {
                    Dictionary<string, string> errors = new();
                    errors.Add("InternalServerError", ex.GetExceptionMessage());
                    error =new ErrorResponse
                    {
                        Status=responseCode,
                        Title="Inernal Server Error",
                        Errors=errors
                    };
                }
                context.Response.StatusCode=responseCode;
               await context.Response.WriteAsJsonAsync(error);
            }
        }


    }
}
