using BackEndTask.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Commands.Dto
{
    public class WorkflowStepUpdateStatusDto
    {
        public Guid? WorkFlowId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? FlowStepId { get; set; }
        public ActionType ActionType { get; set; }
        public ProcessStatus Status { get; set; }
    }
}
