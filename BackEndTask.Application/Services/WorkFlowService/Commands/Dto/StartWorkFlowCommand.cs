using BackEndTask.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Commands.Dto
{
    public class StartWorkFlowCommand:IRequest<ResponseModel<string>>
    {
        public Guid? WorkFlowId { get; set; }
    }
}
