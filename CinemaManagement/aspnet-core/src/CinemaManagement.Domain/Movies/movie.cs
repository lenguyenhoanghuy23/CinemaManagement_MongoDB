using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using CinemaManagement.Genres;
using CinemaManagement.Countries;
using System;
using System.Collections.Generic;

namespace CinemaManagement.Movies;

public class movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string? movieCode { get; set; }
    public string? movieName { get; set; }
    public string? description { get; set; }
    public int runningTime { get; set; }
    public DateTime releaseDate { get; set; }
    public DateTime endDate { get; set; }
    public int year { get; set; }
    public string? producer { get; set; }
    public string? Actor { get; set; }
    public string? Director { get; set; }
    public string[] genres { get; set; }


    public string[] countries { get; set; }
    public string? avata { get; set; }

}
