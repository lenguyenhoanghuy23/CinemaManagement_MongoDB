using CinemaManagement.Status;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaManagement.ShowTimes;

public class showTimeUpdateDto
{
    public DateTime movieSchedule { get; set; }
    public float price { get; set; }

   
    public string movie { get; set; }
    public string screenType { get; set; }
    public string cinemaZoom { get; set; }
}
