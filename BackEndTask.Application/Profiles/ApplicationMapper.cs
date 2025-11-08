using AutoMapper;
using BackEndTask.Application.Services.RolesSerivces.Queries.Dto;
using BackEndTask.Application.Services.SetpsService.Commands.Dto;
using BackEndTask.Application.Services.SetpsService.Queries.Dto;
using BackEndTask.Application.Services.UsersServices.Queries.Dto;
using BackEndTask.Application.Services.WorkFlowService.Commands.Dto;
using BackEndTask.Application.Services.WorkFlowService.Queries.Dto;
using BackEndTask.Data.Entites;
 

namespace BackEndTask.Application.Profiles
{
    public partial class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Step, StepCreateDto>().ReverseMap();
            CreateMap<Step, SetpDto>().ReverseMap();
            CreateMap<ApplicationRole, RoleDto>()
                .ForMember(ele => ele.RoleId, optin => optin.MapFrom(ele => ele.Id))
                .ForMember(ele => ele.RoleName, optin => optin.MapFrom(ele => ele.Name));
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<WorkFlowStep, WorkflowStepCreateDto>().ReverseMap();
            CreateMap<WorkFlow, WorkFlowCreateDto>().ForMember(ele => ele.FlowSteps, option => option.MapFrom(ele => ele.FlowSteps)).ReverseMap();
            CreateMap<WorkFlowStep, WorkFlowStepDto>().ReverseMap();
            CreateMap<WorkFlow, WorkFlowDto>().ForMember(ele => ele.FlowSteps, option => option.MapFrom(ele => ele.FlowSteps)).ReverseMap();
        }
    }
}
