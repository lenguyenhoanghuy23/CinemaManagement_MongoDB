using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CinemaManagement.Countries
{
    public interface ICountrieAppService:IApplicationService
    {
        Task<countrieDto> GetAsync(string countrieCode);
        Task<PagedResultDto<countrieDto>> GetlistAsync(PagedAndSortedResultRequestDto input);
        Task<countrieDto> CreateAsync(countrieCreateDto input);
        Task<countrieDto> UpdateAsync(string countrieCode, countrieUpdateDto input);
        Task DeleteAsync(string countrieCode);
    }
}
