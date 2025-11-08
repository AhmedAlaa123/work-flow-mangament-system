using BackEndTask.Application.Response;
using BackEndTask.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Commands.Dto
{
    public class WorkFlowCreateDto
    {
     
        public string Name { get; set; }
        public string Description { get; set; }
        public FlowStage Stage { get; set; }
        public FlowStatus Status { get; set; }
        public List<WorkflowStepCreateDto> FlowSteps { get; set; }
    }
}
