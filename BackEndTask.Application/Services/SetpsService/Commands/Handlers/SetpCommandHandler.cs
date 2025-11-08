using AutoMapper;
using Azure;
using BackEndTask.Application.Exceptions;
using BackEndTask.Application.Response;
using BackEndTask.Application.Services.SetpsService.Commands.Dto;
using BackEndTask.Application.Services.SetpsService.Queries.Dto;
using BackEndTask.Data.Entites;
using BackEndTask.Persistance.Reposatories.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.SetpsService.Commands.Handlers
{
    public class SetpCommandHandler : IRequestHandler<StepCreateCommand, ResponseModel<SetpDto>>
    {
        private readonly IReposatory<Step> _stepsReposatory;
        private readonly IMapper _mapper;
        public SetpCommandHandler(IReposatory<Step> stepsReposatory, IMapper mapper)
        {
            _stepsReposatory=stepsReposatory;
            _mapper=mapper;
        }

        public async Task<ResponseModel<SetpDto>> Handle(StepCreateCommand request, CancellationToken cancellationToken)
        {
            // check for type
            var dict = new Dictionary<string, string>();
           
           
            if (request.Step.IsHasExternalValidator&&string.IsNullOrEmpty(request.Step.ExtrnalValidatorUrl))
            {
                dict.Add("ExtrnalValidatorUrl", "Extrnal Validator Url Is Required");
            }
            if (dict.Count>0)
            {
                throw new UserFriendlyException(dict);
            }
            var stepModel = _mapper.Map<Step>(request.Step);
            await this._stepsReposatory.InsertAsync(stepModel);
          var afectedRows= await this._stepsReposatory.SaveChangesAsync();
            if (afectedRows<=0)
            {
                return ResponseModel<SetpDto>.Fail(null, "There an Error Occured");
            }
            else
            {
                return ResponseModel<SetpDto>.Success(_mapper.Map<SetpDto>(stepModel));
            }

        }
    }
}
