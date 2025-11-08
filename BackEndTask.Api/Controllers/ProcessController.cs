using Asp.Versioning;
using AutoMapper;
using BackEndTask.Application.Exceptions;
using BackEndTask.Application.Services.WorkFlowService.Commands.Dto;
using BackEndTask.Application.Services.WorkFlowService.Queries.Dto;
using BackEndTask.Data.Enums;
using BackEndTask.Data.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackEndTask.Api.Controllers
{
    [ApiVersion(ApiVersioning.API_VERSION_1)]
    public class ProcessController : BaseController
    {
        public ProcessController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {

        }
        [HttpPut("start")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> StartWorkFlowProcess(Guid?id)
        {
            var response = await _mediator.Send(new StartWorkFlowCommand { WorkFlowId=id });
            if (response.IsSuccess)
            {
                return Ok("Process Started Succeefuly");
            }
            var dict = new Dictionary<string, string>();
            dict.Add("Error", response.ErrorMessage);
            throw new UserFriendlyException(dict);
        }

        [HttpPut("execute")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> ExecuteWorkFlowProcess([FromBody] WorkflowStepUpdateStatusDto workflowStep)
        {
            var response = await _mediator.Send(new WorkflowStepUpdateStatusCommand {  Step=workflowStep });
            if (response.IsSuccess)
            {
                return Ok("Process Changed Action Succeefuly");
            }
            var dict = new Dictionary<string, string>();
            dict.Add("Error", response.ErrorMessage);
            throw new UserFriendlyException(dict);
        }


        [HttpGet("processes")]
        [ProducesResponseType(typeof(List<FlowProcessDto>), 200)]
        public async Task<IActionResult> GetProcess([FromQuery] Guid? FlowId, [FromQuery] Guid? UserId, [FromQuery] ProcessStatus? Status)
        {
            var response = await _mediator.Send(new ProcessQueryDto { FlowId =FlowId, UserId=UserId, Status=Status });
            return Ok(response.Data);
        }



    }
}
