
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace CinemaManagement.Countries;

public class countrie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string countrieCode { get; set; }
    public string countrieName { get; set; }
}
