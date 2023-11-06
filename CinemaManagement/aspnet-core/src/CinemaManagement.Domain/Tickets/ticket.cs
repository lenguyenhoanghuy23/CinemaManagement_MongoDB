using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Tickets
{
    public class ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int typeticker { get; set; }
        public string seatcode { get; set; }
        public int status { get; set; }
        public float priceticker { get; set; }
        public string showtime { get; set; }
    }
}
