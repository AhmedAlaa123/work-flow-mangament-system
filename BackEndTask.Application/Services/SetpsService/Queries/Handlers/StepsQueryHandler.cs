using AutoMapper;
using BackEndTask.Application.Response;
using BackEndTask.Application.Services.SetpsService.Queries.Dto;
using BackEndTask.Data.Entites;
using BackEndTask.Persistance.Reposatories.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace BackEndTask.Application.Services.SetpsService.Queries.Handlers
{
    public class StepsQueryHandler : IRequestHandler<StepQuery, ResponseModel<List<SetpDto>>>
    {
        private readonly IReposatory<Step> _stepsReposatory;
        private readonly IMapper _mapper;
        public StepsQueryHandler(IReposatory<Step> stepsReposatory, IMapper mapper)
        {
            _stepsReposatory=stepsReposatory;
            _mapper=mapper;
        }

        public  async Task<ResponseModel<List<SetpDto>>> Handle(StepQuery request, CancellationToken cancellationToken)
        {
            var steps = await (await _stepsReposatory.GetAllAsync()).Where(ele=>ele.IsActive).ToListAsync();
            return ResponseModel<List<SetpDto>>.Success(_mapper.Map<List<SetpDto>>(steps));
        }
    }
}
