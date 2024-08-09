using FluentValidation;
using GameDashboard.Application.Abstractions;
using GameDashboard.Application.Constants;
using GameDashboard.Application.Repositories;
using GameDashboard.Application.Results;
using GameDashboard.Domain.Concrete;
using MediatR;
using GameDashboard.Domain.Common;
using MongoDB.Driver;

namespace GameDashboard.Application.Features.Commands.CreateBuilding
{
    public class CreateBuildingCommandHandler : IRequestHandler<CreateBuildingCommandRequest, IResult>
    {
        private INoSQLRepository<Building> _buildingRepository;
        private INoSQLRepository<BuildingType> _buildingTypeRepository;
        private IValidator<CreateBuildingCommandRequest> _validator;
        private IMapper _mapper;

        public CreateBuildingCommandHandler(INoSQLRepository<Building> repository, INoSQLRepository<BuildingType> buildingTypeRepository, IValidator<CreateBuildingCommandRequest> validator, IMapper mapper)
        {
            _buildingRepository = repository;
            _buildingTypeRepository = buildingTypeRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(CreateBuildingCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId is null || request.UserId==Guid.Empty)
                return new ErrorResult(ResponseMessages.UserNotFound);


            var valitadeResult = _validator.Validate(request);

            if (!valitadeResult.IsValid) 
            {
                return new ErrorResult(valitadeResult.Errors[0].ErrorMessage);
            }           


            var buildingType = await _buildingTypeRepository.GetAsync(request.BuildingTypeId);

            if(buildingType is null)
            {
                return new ErrorResult(ResponseMessages.BuildingTypeNotFound);
            }

            var building = _mapper.Map<Building>(request);

            var filterBuilder = Builders<Building>.Filter;

            var filter = filterBuilder.And(
                filterBuilder.Ne(b => b.DataStatus, DataStatus.Deleted),
                filterBuilder.Eq(b => b.BuildingTypeId, request.BuildingTypeId),
                filterBuilder.Eq(b => b.UserId, request.UserId));

            var existBuilder = await _buildingRepository.GetAllAsync(filter);

            if(existBuilder is not null && existBuilder.Count>0)
            {
                return new ErrorResult(ResponseMessages.BuildingTypeExist);
            }

            await _buildingRepository.CreateOneAsync(building);

            return new SuccessResult(ResponseMessages.BuildingAdded);
           
        }
    }
}
