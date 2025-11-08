using AutoMapper;
using Azure;
using BackEndTask.Application.Exceptions;
using BackEndTask.Application.Response;
using BackEndTask.Application.Services.WorkFlowService.Commands.Dto;
using BackEndTask.Application.Services.WorkFlowService.Queries.Dto;
using BackEndTask.Data.Entites;
using BackEndTask.Data.Enums;
using BackEndTask.Persistance.Reposatories.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.WorkFlowService.Commands.handlers
{
    public class WorkflowCommandHandler : IRequestHandler<WorkFlowCreateCommand, ResponseModel<WorkFlowDto>>,
        IRequestHandler<StartWorkFlowCommand,ResponseModel<string>>,
        IRequestHandler<WorkflowStepUpdateStatusCommand, ResponseModel<string>>
    {
        private readonly IMapper _mapper;
        private readonly IReposatory<WorkFlow> _workflowRepsatory;
        private readonly IReposatory<WorkFlowStep> _flowStepReposatory;
        public WorkflowCommandHandler(IMapper mapper, IReposatory<WorkFlow> workflowRepsatory, IReposatory<WorkFlowStep> flowStepReposatory)
        {
            _mapper=mapper;
            _workflowRepsatory=workflowRepsatory;
            _flowStepReposatory=flowStepReposatory;
        }

        public async Task<ResponseModel<WorkFlowDto>> Handle(WorkFlowCreateCommand request, CancellationToken cancellationToken)
        {
            // mapp to workflow model
            var workFlowModel = _mapper.Map<WorkFlow>(request.WorkFlow);
            await this._workflowRepsatory.InsertAsync(workFlowModel);
            var affectedRecords=await this._workflowRepsatory.SaveChangesAsync();
            if (affectedRecords==0)
            {
               return ResponseModel<WorkFlowDto>.Fail(null, "No Data Saved");
            }
            return ResponseModel<WorkFlowDto>.Success(data: _mapper.Map<WorkFlowDto>(workFlowModel));

        }

        public async Task<ResponseModel<string>> Handle(StartWorkFlowCommand request, CancellationToken cancellationToken)
        {
            // get work flow
            var workFlow =  await this._workflowRepsatory.GetAll().FirstOrDefaultAsync(ele => ele.Id==request.WorkFlowId);
            if (workFlow is null)
            {
                return ResponseModel<string>.Fail(null,"Work Flow Is Not Registerd");
            }
            workFlow.Stage=Data.Enums.FlowStage.InProgress;
            await this._workflowRepsatory.UpdateAsync(workFlow);
            var rowsAffected = await this._workflowRepsatory.SaveChangesAsync();
            if (rowsAffected<=0)
            {
                return ResponseModel<string>.Fail(null, "Process Not Started");
            }

           var firstStep=await _flowStepReposatory.GetAll().OrderBy(ele => ele.Order).FirstOrDefaultAsync();
            if (firstStep!=null)
            {
                firstStep.Status=ProcessStatus.Active;
            }
            return ResponseModel<string>.Success(null);
        }

        public async Task<ResponseModel<string>> Handle(WorkflowStepUpdateStatusCommand request, CancellationToken cancellationToken)
        {
             var dict = new Dictionary<string, string>();
            var step =await _flowStepReposatory.GetAll().Where(ele => ele.Id==request.Step.FlowStepId&&ele.ActionType==ActionType.NoAction).FirstOrDefaultAsync();
            if (step is null)
            {
                return ResponseModel<string>.Success("Step Updated Successfuly");
            }
            // get work flow
            var workFlow =await this._workflowRepsatory.GetAll().FirstOrDefaultAsync(ele => ele.Id==request.Step.WorkFlowId);
            if (workFlow is null)
            {
                dict.Add("WorkFlowNotRegister", "Work Flow Not Found");
                throw new UserFriendlyException(dict);
            }
            // check if closed or not
            if (workFlow.Status!=FlowStatus.UnderReview&&workFlow.Stage!=FlowStage.Closed)
            {
                dict.Add("FlowClosed", "Work Flow Is Closed");
                throw new UserFriendlyException(dict);
            }

            // check if current users is that assigned to it and must all before is take an action
            bool hasPreviousNoActionSteps = _flowStepReposatory.GetAll()
    .Where(ele => ele.Order < step!.Order && ele.ActionType == ActionType.NoAction)
    .Count() > 0;
            if (hasPreviousNoActionSteps)
            {
                dict.Add("CurrentStepNotValid", "There Are another Step Befor This Step Not Take Any Action On It");
                throw new UserFriendlyException(dict);
            }
            if (step.UserId!=request.Step.UserId)
            {
                dict.Add("CurrentUserIsnotAssignedToThisStep", "Current User Is Not Assigned To This Step");
                throw new UserFriendlyException(dict);
            }
            step.ActionType=request.Step.ActionType;
            step.Status=request.Step.Status;
            await _flowStepReposatory.UpdateAsync(step);
            await _flowStepReposatory.SaveChangesAsync();
           var nextStep=await _flowStepReposatory.GetAll().Where(ele => ele.Order>step.Order && (ele.ActionType==Data.Enums.ActionType.NoAction)).OrderByDescending(ele => ele.Order).FirstOrDefaultAsync();
            if (nextStep is null)
            {
                // this is last step
                if (request.Step.ActionType==ActionType.Approve)
                {
                    workFlow.Status= FlowStatus.Accepted;
                }
                else if (request.Step.ActionType==ActionType.Reject)
                {
                    workFlow.Status= FlowStatus.Rejected;
                }
                workFlow.Stage=FlowStage.Closed;
            }
            else
            {
                workFlow.Stage=FlowStage.InProgress;
            }

            await this._workflowRepsatory.UpdateAsync(workFlow);
            await _workflowRepsatory.SaveChangesAsync();

            return ResponseModel<string>.Success("Step Updated Successfuly");
        }
    }
}
