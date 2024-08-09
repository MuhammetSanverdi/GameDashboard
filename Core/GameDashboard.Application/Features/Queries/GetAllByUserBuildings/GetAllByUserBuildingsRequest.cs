using GameDashboard.Application.Results;
using GameDashboard.Domain.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Features.Queries.GetAllByUserBuildings
{
    public class GetAllByUserBuildingsRequest:IRequest<IDataResult<IList<GetAllByUserBuildingsResponse>>>
    {
        public Guid UserId { get; set; }
    }
}
