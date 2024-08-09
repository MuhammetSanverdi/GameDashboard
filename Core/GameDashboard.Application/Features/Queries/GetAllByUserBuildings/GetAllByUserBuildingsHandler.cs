using GameDashboard.Application.Abstractions;
using GameDashboard.Application.Constants;
using GameDashboard.Application.Repositories;
using GameDashboard.Application.Results;
using GameDashboard.Domain.Concrete;
using MediatR;
using MongoDB.Driver;

namespace GameDashboard.Application.Features.Queries.GetAllByUserBuildings
{
    public class GetAllByUserBuildingsHandler : IRequestHandler<GetAllByUserBuildingsRequest, IDataResult<IList<GetAllByUserBuildingsResponse>>>
    {
        private INoSQLRepository<Building> _buildingRepository;
        private INoSQLRepository<BuildingType> _buildingTypeRepository;
        private IMapper _mapper;
        public GetAllByUserBuildingsHandler(INoSQLRepository<Building> repository, IMapper mapper, INoSQLRepository<BuildingType> buildingTypeRepository)
        {
            _buildingRepository = repository;
            _mapper = mapper;
            _buildingTypeRepository = buildingTypeRepository;
        }

        public async Task<IDataResult<IList<GetAllByUserBuildingsResponse>>> Handle(GetAllByUserBuildingsRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                return new ErrorDataResult<IList<GetAllByUserBuildingsResponse>>(ResponseMessages.UserNotFound);
            }
            var filterBuilder = Builders<Building>.Filter;

            var filter = filterBuilder.Eq(b=>b.UserId,request.UserId);

            var buildings = await _buildingRepository.GetAllAsync(filter);

            var buildingTypes = await _buildingTypeRepository.GetAllAsync();

            var buildingsDto = _mapper.Map<IList<GetAllByUserBuildingsResponse>>(buildings);

            foreach (var item in buildingsDto)
            {

                item.BuildingTypeName = buildingTypes.FirstOrDefault(b=>b.Id==item.BuildingTypeId).Name;
            }

            return new SuccessDataResult<IList<GetAllByUserBuildingsResponse>>(buildingsDto);
               

        }
    }
}
