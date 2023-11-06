using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaManagement.Tickets
{
    public class ticketDto
    {
        public string Id { get; set; }
        public int typeticker { get; set; }
        public string seatcode { get; set; }
        public int status { get; set; }
        public float priceticker { get; set; }
        public float price { get; set; }
        public string showtime { get; set; }
    }
}
