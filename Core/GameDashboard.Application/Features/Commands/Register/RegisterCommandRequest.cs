using GameDashboard.Application.Repositories;
using GameDashboard.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Features.Commands.Register
{
    /// <summary>
    /// User register request
    /// </summary>
    public class RegisterCommandRequest:IRequest<IResult>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
