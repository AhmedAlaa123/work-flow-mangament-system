using Asp.Versioning;
using AutoMapper;
using BackEndTask.Application.Exceptions;
using BackEndTask.Application.Services.WorkFlowService.Commands.Dto;
using BackEndTask.Application.Services.WorkFlowService.Queries.Dto;
using BackEndTask.Data.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackEndTask.Api.Controllers
{
    [ApiVersion(ApiVersioning.API_VERSION_1)]
    public class WorkflowController : BaseController
    {
        public WorkflowController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {

        }

        [HttpPost("create-workflow")]
        [ProducesResponseType(typeof(WorkFlowDto), 200)]
        public async Task<IActionResult> CreateWorkFlow([FromBody]WorkFlowCreateDto workFlow)
        {
            var response =await _mediator.Send(new WorkFlowCreateCommand() { WorkFlow=workFlow });
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            var dict = new Dictionary<string, string>();
            dict.Add("Error", response.ErrorMessage);
            throw new UserFriendlyException(dict);
        }
    }
}
