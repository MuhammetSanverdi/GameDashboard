using GameDashboard.Application.Results;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Features.Commands.CreateBuilding
{
    public class CreateBuildingCommandRequest:IRequest<IResult>
    {
        public Guid? UserId { get; set; }
        public string BuildingTypeId { get; set; }
        public double Cost { get; set; }
        public int ConstructionTime { get; set; }

    }
}
