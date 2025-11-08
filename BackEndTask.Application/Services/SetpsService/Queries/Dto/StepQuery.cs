using BackEndTask.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.SetpsService.Queries.Dto
{
    public class StepQuery:IRequest<ResponseModel<List<SetpDto>>>
    {
    }
}
