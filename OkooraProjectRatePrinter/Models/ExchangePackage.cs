using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OkooraProjectRatePrinter.Models
{
    public class ExchangePackage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("Rates")]
        public List<ExchangeRate> Rates { get; set; } = new List<ExchangeRate>();
    }
}
