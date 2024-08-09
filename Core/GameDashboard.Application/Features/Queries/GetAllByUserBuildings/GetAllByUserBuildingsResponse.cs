namespace GameDashboard.Application.Features.Queries.GetAllByUserBuildings
{
    public class GetAllByUserBuildingsResponse
    {
        public string Id { get; set; }
        public string BuildingTypeId { get; set; }
        public string BuildingTypeName { get; set; }
        public double Cost { get; set; }
        public int ConstructionTime { get; set; }
    }
}
