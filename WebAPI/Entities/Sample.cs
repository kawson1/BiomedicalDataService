using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.Models
{
    [Serializable]
    public class Sample
    {
        [BsonId]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public SensorType SensorType { get; set; }
        public int SensorId { get; set; }
        public int Value { get; set; }
    }
}
