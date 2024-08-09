using GameDashboard.Application.Repositories;
using GameDashboard.Application.Results;
using GameDashboard.Domain.Common;
using GameDashboard.Domain.Concrete;
using MediatR;
using MongoDB.Driver;

namespace GameDashboard.Application.Features.Commands.GetAllBuildingTypes
{
    public class GetAllBuildingTypesQueryHandler : IRequestHandler<GetAllBuildingTypesQueryRequest, IDataResult<IList<BuildingType>>>
    {

        private INoSQLRepository<BuildingType> _buildingTypeRepository;
        private INoSQLRepository<Building> _buildingRepository;


        public GetAllBuildingTypesQueryHandler(INoSQLRepository<BuildingType> repository, INoSQLRepository<Building> buildingRepository)
        {
            _buildingTypeRepository = repository;
            _buildingRepository = buildingRepository;
        }


        public async Task<IDataResult<IList<BuildingType>>> Handle(GetAllBuildingTypesQueryRequest request, CancellationToken cancellationToken)
        {
            var buildingTypeFilterBuilder = Builders<BuildingType>.Filter;

            var buildingTypeFilter = buildingTypeFilterBuilder.And(
                buildingTypeFilterBuilder.Ne(b=>b.DataStatus,DataStatus.Deleted),
                buildingTypeFilterBuilder.Eq(b => b.IsOpen, true));

            var buildingTypes = await _buildingTypeRepository.GetAllAsync(buildingTypeFilter);
            
            var buildingFilterBuilder = Builders<Building>.Filter;
            var buildingFilter = buildingFilterBuilder.And(
                buildingFilterBuilder.Ne(b => b.DataStatus, DataStatus.Deleted),
                buildingFilterBuilder.Eq(b => b.UserId, new Guid(request.UserId)));

            var buildings = await _buildingRepository.GetAllAsync(buildingFilter);
            var usedBuildingTypeIds = buildings.Select(b => b.BuildingTypeId).ToList();

            var filteredBuildingTypes = buildingTypes.Where(b=>!usedBuildingTypeIds.Contains(b.Id));

            return new SuccessDataResult<IList<BuildingType>>(filteredBuildingTypes.ToList());
        }
        
    }
}

