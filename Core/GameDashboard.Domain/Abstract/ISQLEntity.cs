namespace GameDashboard.Domain.Abstract
{
    public interface ISQLEntity:IEntity
    {
        public Guid Id { get; set; }
    }
}
