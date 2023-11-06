using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CinemaManagement.Tickets
{
    public interface ITicketAppService:IApplicationService
    {
        Task<ticketDto> CreateAsync(string showTimeCode);
        Task DeleteAsync(string showTimeCode);

        Task<PagedResultDto<ticketDto>> GetAsync(string showtimeCode);
    }
}
