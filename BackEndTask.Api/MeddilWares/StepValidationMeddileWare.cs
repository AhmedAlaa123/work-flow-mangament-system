using Azure;
using BackEndTask.Application.BaseDto;
using BackEndTask.Application.Exceptions;
using BackEndTask.Application.Services.WorkFlowService.Commands.Dto;
using BackEndTask.Application.Services.WorkFlowService.Queries.Dto;
using BackEndTask.Data.Entites;
using BackEndTask.Persistance.Reposatories.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text.Json;

namespace BackEndTask.Api.MeddilWares
{
    public class StepValidationMeddileWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<StepValidationMeddileWare> _logger;
      
        private readonly IHttpClientFactory httpClientFactory;
        public StepValidationMeddileWare(RequestDelegate next, ILogger<StepValidationMeddileWare> logger,   IHttpClientFactory httpClientFactory )
        {
            _next = next;
            _logger = logger;
 

            this.httpClientFactory=httpClientFactory;
           
        }
        public async Task InvokeAsync(HttpContext context, IMediator _mediator)
        {
            // Extract workflow data from request
            var controllerActionDescriptor= context.GetEndpoint().Metadata
                .GetMetadata<ControllerActionDescriptor>();
            
            var controllerName = controllerActionDescriptor.ControllerName;
            var actionName = controllerActionDescriptor.ActionName;
            if (!String.IsNullOrEmpty(controllerName)&&!String.IsNullOrEmpty(actionName)&&controllerName.ToLower().Trim().Equals("process")&&actionName.ToLower().Trim().Equals("ExecuteWorkFlowProcess".ToLower()))
            {
              var stepInfo= await ExtractWorkflowStepData(context);
                if (stepInfo is not null)
                {
                    var step = (await _mediator.Send(new WorkFlowStepQueryDto { Id=stepInfo.FlowStepId })).Data;
                    if (step!=null)
                    {

                        // check if this step has extenal end point
                        if (step.Step.IsHasExternalValidator)
                        {
                            if (string.IsNullOrEmpty(step.Step.ExtrnalValidatorUrl))
                            {
                                var dict = new Dictionary<string, string>();
                                dict.Add("ExtrnalValidatorUrl", "Extranl Validator Url Is Required");
                                throw new UserFriendlyException(dict);
                            }

                             var client =this.httpClientFactory.CreateClient("ExternalValidation");
                             var respons= await client.PostAsJsonAsync<object>(step.Step.ExtrnalValidatorUrl, new {FlowId=step.WorkFlowId});
                            if (respons.IsSuccessStatusCode)
                            {
                                var responseDto = await respons.Content.ReadFromJsonAsync<ExtranlValidationReponseDto>();
                                if (responseDto.IsAccepted)
                                {
                                    await _next(context);
                                }
                                else
                                {
                                    var dict = new Dictionary<string, string>();
                                    dict.Add("ProcessRejected", responseDto.Resone);
                                    throw new UserFriendlyException(dict);
                                }

                            }
                            else
                            {
                                var dict = new Dictionary<string, string>();
                                var errorContent = await respons.Content.ReadAsStringAsync();
                                dict.Add("ErrorFromExtrenal", errorContent);
                                throw new UserFriendlyException(dict);
                            }
                        }
                        

                    }
                }
            }
            // Perform validation
            //var validationResult = await validationService.ValidateStepAsync(workflowData);

            //if (!validationResult.IsValid)
            //{
            //    context.Response.StatusCode = StatusCodes.Status400BadRequest;
            //    await context.Response.WriteAsJsonAsync(new
            //    {
            //        Error = "Validation failed",
            //        Details = validationResult.Errors
            //    });
            //    return;
            //}

            // If validation passes, continue to next middleware
            await _next(context);
        }
        private async Task<WorkflowStepUpdateStatusDto> ExtractWorkflowStepData(HttpContext context)
        {
            // Extract data based on your workflow structure
            if (context.Request.Method == "POST" || context.Request.Method == "PUT")
            {
                context.Request.EnableBuffering();
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                return JsonSerializer.Deserialize<WorkflowStepUpdateStatusDto>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return null;
        }
    }
}
