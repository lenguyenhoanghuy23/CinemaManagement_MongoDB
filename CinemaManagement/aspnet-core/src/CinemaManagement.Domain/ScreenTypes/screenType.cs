using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using Volo.Abp.Domain.Entities;

namespace CinemaManagement.ScreenTypes;

public class screenType
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string  screenCode { get; set; }
    public string  screenName { get; set; }
}
