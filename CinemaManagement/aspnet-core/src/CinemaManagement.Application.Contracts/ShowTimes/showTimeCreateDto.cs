using CinemaManagement.Status;
using System;


namespace CinemaManagement.ShowTimes
{
    public class showTimeCreateDto
    {
       
        public string showTimeCode { get; set; }

        public DateTime movieSchedule { get; set; }
        public float price { get; set; }
        public string movie { get; set; }
        public string screenType { get; set; }
        public string cinemaZoom { get; set; }
    }
}
