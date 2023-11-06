using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using CinemaManagement.ScreenTypes;
using CinemaManagement.Status;

namespace CinemaManagement.CinemaZooms
{
    public class cinemaZoomDto
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string cinemaCode { get; set; }
        public string cinemaName { get; set; }
        public int numberSeats { get; set; }
        public int rowSeats { get; set; }
        public int columnSeats { get; set; }
        public StatusEmu status { get; set; }
   
    }
}
