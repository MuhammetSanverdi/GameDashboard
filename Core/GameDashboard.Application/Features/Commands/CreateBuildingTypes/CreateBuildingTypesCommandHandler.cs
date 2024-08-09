using GameDashboard.Application.Repositories;
using GameDashboard.Application.Results;
using GameDashboard.Domain.Concrete;
using MediatR;
using MongoDB.Driver;

namespace GameDashboard.Application.Features.Commands.CreateBuildingTypes
{
    /// <summary>
    /// Adds seed data from Building Types Handler
    /// </summary>
    public class CreateBuildingTypesCommandHandler : IRequestHandler<CreateBuildingTypesCommandRequest, IResult>
    {
        private INoSQLRepository<BuildingType> _repository;


        public CreateBuildingTypesCommandHandler(INoSQLRepository<BuildingType> repository)
        {
            _repository = repository;
        }


        public async Task<IResult> Handle(CreateBuildingTypesCommandRequest request, CancellationToken cancellationToken)
        {
            var buildingTypes = new[]
            {
                new BuildingType
                {
                    Id = "66b11c85693367f75149ad63",
                    Name = "Farm",
                    IsOpen = true,
                    CreatedAt = DateTime.UtcNow
                },
                new BuildingType
                {
                    Id = "66b11c8a4ab06ab0ffa5c985",
                    Name = "Academy",
                    IsOpen = true,
                    CreatedAt = DateTime.UtcNow
                },
                new BuildingType
                {
                    Id = "66b11c8f902dcd755d9fa9ba",
                    Name = "Headquarters",
                    IsOpen = true,
                    CreatedAt = DateTime.UtcNow
                },
                new BuildingType
                {
                    Id = "66b11c940bdbde8f99cd10c4",
                    Name = "LumberMill",
                    IsOpen = true,
                    CreatedAt = DateTime.UtcNow
                },
                new BuildingType
                {
                    Id = "66b11c999f582f57af20b6bc",
                    Name = "Barracks",
                    IsOpen = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
           
            try
            {
                await _repository.CreateManyAsync(buildingTypes);

                return new SuccessResult("Seed data added");
            }
            catch (MongoException e)
            {
                if (e.GetType().Name.Contains(nameof(MongoBulkWriteException)))
                {
                    return new ErrorResult("Seed data already added");
                }
                throw e;
            }
            
        }
    }
}

