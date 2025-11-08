using AutoMapper;
using BackEndTask.Application.Response;
using BackEndTask.Application.Services.UsersServices.Queries.Dto;
using BackEndTask.Data.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.UsersServices.Queries
{
    public class UsersQueryHandler : IRequestHandler<UserQuery, ResponseModel<List<UserDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UsersQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager=userManager;
            _mapper=mapper;
        }

        public async Task<ResponseModel<List<UserDto>>> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.ToListAsync();
            return ResponseModel<List<UserDto>>.Success(_mapper.Map<List<UserDto>>(users));
        }
    }
}
