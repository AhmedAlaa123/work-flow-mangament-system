using BackEndTask.Application.Response;
using BackEndTask.Data.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Queries.Dto
{
    public class WorkFlowStepQueryDto : IRequest<ResponseModel<WorkFlowStep>>
    {
        public Guid? Id { get; set; }
    }
}
