using GameDashboard.Domain.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameDashboard.Domain.Common
{
    public class MongoEntity : INoSQLEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [BsonElement("data_status")]
        public DataStatus DataStatus { get; set; }
    }
}
