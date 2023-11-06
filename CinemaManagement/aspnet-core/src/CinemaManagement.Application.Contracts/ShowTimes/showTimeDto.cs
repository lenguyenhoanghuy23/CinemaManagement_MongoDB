using CinemaManagement.Status;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaManagement.ShowTimes
{
    public class showTimeDto
    {
      
        public string showTimeCode { get; set; }

        public DateTime movieSchedule { get; set; }
        public float price { get; set; }

        public int status { get; set; }
        public string movie { get; set; }
        public string  movieAvata { get; set; }
        public string  movieName { get; set; }
        public string screenType { get; set; }
        public string cinemaZoom { get; set; }
    }
}
