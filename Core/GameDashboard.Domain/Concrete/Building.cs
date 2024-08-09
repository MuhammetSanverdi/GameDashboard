using GameDashboard.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Domain.Concrete
{
    public class Building:MongoEntity
    {
        [BsonElement("user_id")]
        public Guid UserId { get; set; }

        [BsonElement("building_type_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BuildingTypeId { get; set; }

        [BsonElement("cost")]
        public double Cost { get; set; }

        [BsonElement("construction_time")]
        public int ConstructionTime { get; set; }
   
    }
}
