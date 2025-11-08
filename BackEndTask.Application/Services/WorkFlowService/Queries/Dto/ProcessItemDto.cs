using BackEndTask.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Queries.Dto
{
    public class ProcessItemDto
    {
        public string Name{ get;set;}
        public ProcessStatus Status { get; set; }
        public int Order { get; set; }
    }
}
