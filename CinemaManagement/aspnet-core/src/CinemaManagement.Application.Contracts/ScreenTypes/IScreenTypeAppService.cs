
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CinemaManagement.ScreenTypes
{
    public interface IScreenTypeAppService : IApplicationService
    {
        Task<screenTypeDto> GetAsync(string screenCode);
        Task<PagedResultDto<screenTypeDto>> GetlistAsync(PagedAndSortedResultRequestDto input);
        Task<screenTypeDto> CreateAsync(screenTypeCreateDto input);
        Task<screenTypeDto> UpdateAsync(string screenCode,screenTypeUpdateDto input);
        Task DeleteAsync(string screenCode);
    }
}
