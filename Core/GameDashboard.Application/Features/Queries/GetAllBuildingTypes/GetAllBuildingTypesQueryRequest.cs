using GameDashboard.Application.Results;
using GameDashboard.Domain.Concrete;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Features.Commands.GetAllBuildingTypes
{
    /// <summary>
    /// Adds seed data from Building Types
    /// </summary>
    public class GetAllBuildingTypesQueryRequest : IRequest<IDataResult<IList<BuildingType>>>
    {
        public string UserId { get; set; }
    }
}

