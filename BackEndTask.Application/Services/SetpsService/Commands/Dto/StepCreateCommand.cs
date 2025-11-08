using BackEndTask.Application.Response;
using BackEndTask.Application.Services.SetpsService.Queries.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.SetpsService.Commands.Dto
{
    public class StepCreateCommand:IRequest<ResponseModel<SetpDto>>
    {
        public StepCreateDto Step { get; set; }
    }
}
