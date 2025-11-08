using BackEndTask.Application.Services.WorkFlowService.Commands.Dto;
using BackEndTask.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Queries.Dto
{
    public class WorkFlowDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FlowStage Stage { get; set; }
        public FlowStatus Status { get; set; }
        public List<WorkFlowStepDto> FlowSteps { get; set; }
    }
}
