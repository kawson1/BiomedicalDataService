using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.Models
{
    public class Sample
    {
        [BsonId]
//        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public SensorType SensorType { get; set; }
        public int SensorId { get; set; }
        public int Value { get; set; }
    }
}
