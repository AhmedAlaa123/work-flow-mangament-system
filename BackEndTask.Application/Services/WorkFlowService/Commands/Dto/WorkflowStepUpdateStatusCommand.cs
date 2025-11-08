using BackEndTask.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Commands.Dto
{
    public class WorkflowStepUpdateStatusCommand:IRequest<ResponseModel<string>>
    {
        public WorkflowStepUpdateStatusDto Step { get; set; }
    }
}
