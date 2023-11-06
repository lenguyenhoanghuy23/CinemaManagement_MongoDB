
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CinemaManagement.ShowTimes
{
    public interface IShowTimeAppsService:IApplicationService
    {
        Task<showTimeDto> GetAsync(string showtimeCode);
        Task<PagedResultDto<showTimeDto>> GetlistAsync(PagedAndSortedResultRequestDto input);
        Task<showTimeDto> CreateAsync(showTimeCreateDto input);
        Task<showTimeDto> UpdateAsync(string showtimeCode, showTimeUpdateDto input);
        Task DeleteAsync(string showtimeCode);
    }
}
