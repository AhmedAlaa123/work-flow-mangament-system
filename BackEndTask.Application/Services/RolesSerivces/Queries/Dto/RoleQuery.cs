using BackEndTask.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.RolesSerivces.Queries.Dto
{
    public class RoleQuery:IRequest<ResponseModel<List<RoleDto>>>
    {
    }
}
