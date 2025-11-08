using BackEndTask.Application.Response;
using BackEndTask.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Queries.Dto
{
    public class ProcessQueryDto:IRequest<ResponseModel<List<FlowProcessDto>>>
    {
        public Guid? FlowId { get; set; }
        public Guid? UserId { get; set; }
        public ProcessStatus? Status { get; set; }
    }
}
