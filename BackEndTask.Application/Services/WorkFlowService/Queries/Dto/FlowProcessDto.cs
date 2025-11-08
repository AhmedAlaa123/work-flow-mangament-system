using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Queries.Dto
{
    public class FlowProcessDto
    {
        public string FlowName { get; set; }
        public List<ProcessItemDto> Steps { get; set; }


    }
}
