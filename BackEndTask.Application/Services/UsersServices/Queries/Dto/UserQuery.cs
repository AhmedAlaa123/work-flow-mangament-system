using BackEndTask.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Application.Services.UsersServices.Queries.Dto
{
    public class UserQuery:IRequest<ResponseModel<List<UserDto>>>
    {
    }
}
