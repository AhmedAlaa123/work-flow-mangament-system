using BackEndTask.Data.Entites;
using BackEndTask.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Commands.Dto
{
 
    public class WorkflowStepCreateDto
    {
        public Guid? WorkFlowId { get; set; }
        public Guid? StepId { get; set; }
        public Guid? UserId { get; set; }
        public ActionType ActionType { get; set; }
        public ProcessStatus Status { get; set; }
        public int Order { get; set; }
    }
}
