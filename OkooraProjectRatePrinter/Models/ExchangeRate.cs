using MongoDB.Bson.Serialization.Attributes;

namespace OkooraProjectRatePrinter.Models
{
    public class ExchangeRate
    {
        [BsonElement("FromCurrency")]
        public string FromCurrency { get; set; }

        [BsonElement("ToCurrency")]
        public string ToCurrency { get; set; }

        [BsonElement("Value")]
        public decimal Value { get; set; }

        [BsonElement("LastUpdate")]
        public DateTime LastUpdate { get; set; }
    }
}
