
using CinemaManagement.ShowTimes;
using CinemaManagement.Tickets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace CinemaManagement.Seller
{
    public interface ISellerAppService
    {
        Task<PagedResultDto<showTimeDto>> GetlistSchedulerAsync(DateTime schedule);
       
        Task  updateAsync(List<ticketUpdateDto> input);

    }
}
