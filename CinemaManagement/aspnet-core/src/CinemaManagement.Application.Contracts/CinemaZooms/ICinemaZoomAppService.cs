using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CinemaManagement.CinemaZooms
{
    public interface ICinemaZoomAppService:IApplicationService
    {
        Task<cinemaZoomDto> GetAsync(string cinemaCode);
        Task<PagedResultDto<cinemaZoomDto>> GetlistAsync(PagedAndSortedResultRequestDto input);
        Task<cinemaZoomDto> CreateAsync(cinemaZoonCreateDto input);
        Task<cinemaZoomDto> UpdateAsync(string cinemaCode, cinemaZoomUpdateDto input);
        Task DeleteAsync(string cinemaCode);
    }
}
