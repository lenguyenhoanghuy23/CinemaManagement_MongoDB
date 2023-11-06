using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CinemaManagement.Genres;

public class genre
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string genreCode { get; set; }
    public string genreName { get; set; }

}
