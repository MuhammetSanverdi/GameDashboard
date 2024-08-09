using GameDashboard.Application.Results;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Features.Commands.CreateBuildingTypes
{
    /// <summary>
    /// Adds seed data from Building Types request
    /// </summary>
    public class CreateBuildingTypesCommandRequest : IRequest<IResult>
    {
    }
}

