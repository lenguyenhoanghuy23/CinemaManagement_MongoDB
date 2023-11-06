using CinemaManagement.Status;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.ShowTimes
{
    public class showTime
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string showTimeCode { get; set; }

        public DateTime movieSchedule { get; set; }
        public float price { get; set; }

        public int status { get; set; }
        public string movie { get; set; }

        public string cinemaZoom { get; set; }
        public string screenType { get; set; }

    }
}
