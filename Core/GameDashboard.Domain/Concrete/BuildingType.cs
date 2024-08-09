using GameDashboard.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameDashboard.Domain.Concrete
{
    public class BuildingType : MongoEntity
    {

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("is_open")]
        public bool IsOpen { get; set; }
    }
}
