using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Locally.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("Favorites")]
        public Favorites? Favorites { get; set; } = null!;
    }
}
