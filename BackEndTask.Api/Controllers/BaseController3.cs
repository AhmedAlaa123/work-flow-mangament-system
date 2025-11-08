using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackEndTask.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BaseController:ControllerBase
    {
        protected IMediator _mediator { get; set; }
        protected IMapper _mapper;
        public BaseController(IMediator mediator, IMapper mapper)
        {
            _mediator=mediator;
            _mapper=mapper;
        }


    }
}
