using GameDashboard.Application.Results;
using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Features.Commands.Login
{
    public class LoginCommandRequest:IRequest<IDataResult<LoginCommandResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
