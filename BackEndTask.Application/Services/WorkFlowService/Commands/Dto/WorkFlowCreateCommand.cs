using BackEndTask.Application.Response;
using BackEndTask.Application.Services.WorkFlowService.Queries.Dto;
using MediatR;
 
namespace BackEndTask.Application.Services.WorkFlowService.Commands.Dto
{
    public class WorkFlowCreateCommand : IRequest<ResponseModel<WorkFlowDto>>
    {
        public WorkFlowCreateDto WorkFlow { get; set; }
    }
}
