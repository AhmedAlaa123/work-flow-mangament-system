using Asp.Versioning;
using AutoMapper;
using BackEndTask.Application.Exceptions;
using BackEndTask.Application.Services.RolesSerivces.Queries.Dto;
using BackEndTask.Application.Services.SetpsService.Commands.Dto;
using BackEndTask.Application.Services.SetpsService.Queries.Dto;
using BackEndTask.Data.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackEndTask.Api.Controllers
{
    [ApiVersion(ApiVersioning.API_VERSION_1)]
    public class StepsController : BaseController
    {
        public StepsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
        [HttpPost("create-step")]
        [ProducesResponseType(typeof(SetpDto), 200)]
        public async Task<IActionResult> CreateNewStep([FromBody] StepCreateDto step)
        {
            var response = await _mediator.Send(new StepCreateCommand { Step=step });
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            var dict = new Dictionary<string, string>();
            dict.Add("Error", response.ErrorMessage);
            throw new UserFriendlyException(dict);
        }
        [HttpGet("steps-list")]
        [ProducesResponseType(typeof(List<SetpDto>), 200)]
        public async Task<IActionResult> GetStepsList( )
        {
            var response = await _mediator.Send(new StepQuery());
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
