using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderWebApi.Model
{
    [Serializable, BsonIgnoreExtraElements]
    public class Order
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("customer_id"), BsonRepresentation(BsonType.Int32)]
        public int CustomerId { get; set; }
        [BsonElement("order_on"), BsonRepresentation(BsonType.DateTime)]
        public DateTime OrderOn { get; set; }
        [BsonElement("order_details")]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
