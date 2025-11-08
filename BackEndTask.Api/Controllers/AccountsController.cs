using Asp.Versioning;
using AutoMapper;
using BackEndTask.Application.Services.RolesSerivces.Queries.Dto;
using BackEndTask.Application.Services.UsersServices.Queries.Dto;
using BackEndTask.Data.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackEndTask.Api.Controllers
{
    [ApiVersion(ApiVersioning.API_VERSION_1)]
    public class AccountsController : BaseController
    {
        public AccountsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
        [HttpGet("roles-list")]
        [ProducesResponseType(typeof(RoleDto),200)]
        public async Task<IActionResult> GetRolesList()
        {
            var rolesReponse =await _mediator.Send(new RoleQuery());
            return Ok(rolesReponse.Data);
        }

        [HttpGet("users-list")]
        [ProducesResponseType(typeof(UserDto), 200)]
        public async Task<IActionResult> GetUsersList()
        {
            var usersReponse = await _mediator.Send(new UserQuery());
            return Ok(usersReponse.Data);
        }
    }
}
    