using BackEndTask.Application.Response;
using BackEndTask.Application.Services.WorkFlowService.Queries.Dto;
using BackEndTask.Data.Entites;
using BackEndTask.Persistance.Reposatories.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Queries
{
    public class WorkFlowQueryHandler : IRequestHandler<ProcessQueryDto, ResponseModel<List<FlowProcessDto>>>,
         IRequestHandler<WorkFlowStepQueryDto, ResponseModel<WorkFlowStep>>
    {
        private readonly IReposatory<WorkFlow> _workflowRepsatory;
        private readonly IReposatory<WorkFlowStep> _stepRepos;

        public WorkFlowQueryHandler(IReposatory<WorkFlow> workflowRepsatory, IReposatory<WorkFlowStep> stepRepos)
        {
            _workflowRepsatory=workflowRepsatory;
            _stepRepos=stepRepos;
        }

        public async Task<ResponseModel<List<FlowProcessDto>>> Handle(ProcessQueryDto request, CancellationToken cancellationToken)
        {
            var flows = this._workflowRepsatory.GetAll();
            if (request.FlowId!=null)
            {
                flows=flows.Where(ele => ele.Id==request.FlowId);
            }
            flows=flows.Include(ele => ele.FlowSteps).ThenInclude(ele => ele.Step);
            var result =await flows.GroupBy(ele => ele).Select(ele => new FlowProcessDto
            {
                FlowName=ele.Key.Name,
                Steps=ele.SelectMany(item => item.FlowSteps).Where(item => (item.UserId==request.UserId||request.UserId==null)&&(item.Status==request.Status||request.Status==null)).Select(item => new ProcessItemDto
                {
                    Name=item.Step.StepName,
                    Order=item.Order,
                    Status=item.Status
                }).ToList()
            }).ToListAsync(); ;
            return ResponseModel<List<FlowProcessDto>>.Success(result);
        }

        public async Task<ResponseModel<WorkFlowStep>> Handle(WorkFlowStepQueryDto request, CancellationToken cancellationToken)
        {
            var step = await _stepRepos.GetAll().Include(ele=>ele.Step).FirstOrDefaultAsync(ele => ele.Id==request.Id);

            return ResponseModel<WorkFlowStep>.Success(step);
        }
    }
}
