using CinemaManagement.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace CinemaManagement.revenues
{
    public interface IRevenuesAppservice
    {
        float totalCinemaCost( );

        float Totalticketssoldperweek( );
    }
}
