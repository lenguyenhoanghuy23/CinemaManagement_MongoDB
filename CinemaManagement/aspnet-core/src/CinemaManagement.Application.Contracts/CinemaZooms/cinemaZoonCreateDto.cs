using CinemaManagement.ScreenTypes;
using CinemaManagement.Status;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaManagement.CinemaZooms
{
    public class cinemaZoonCreateDto
    {
        public string cinemaCode { get; set; }
        public string cinemaName { get; set; }
        public int numberSeats { get; set; }
        public int rowSeats { get; set; }
        public int columnSeats { get; set; }
        public StatusEmu status { get; set; }
    
    }
}
