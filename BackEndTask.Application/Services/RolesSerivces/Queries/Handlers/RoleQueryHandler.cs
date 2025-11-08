using AutoMapper;
using BackEndTask.Application.Response;
using BackEndTask.Application.Services.RolesSerivces.Queries.Dto;
using BackEndTask.Data.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.RolesSerivces.Queries.Handlers
{
    public class RoleQueryHandler : IRequestHandler<RoleQuery, ResponseModel<List<RoleDto>>>
    {
        private readonly RoleManager<ApplicationRole> _rolemanger;
        private readonly IMapper _mapper;

        public RoleQueryHandler(RoleManager<ApplicationRole> rolemanger, IMapper mapper)
        {
            _rolemanger=rolemanger;
            _mapper=mapper;
        }

        public async Task<ResponseModel<List<RoleDto>>> Handle(RoleQuery request, CancellationToken cancellationToken)
        {
            var roles =await _rolemanger.Roles.ToListAsync();

            return ResponseModel<List<RoleDto>>.Success(data: _mapper.Map<List<RoleDto>>(roles));
        }
    }
}
