
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace CinemaManagement.Movies
{
    public interface IMovieAppService
    {
        Task<movieDto> GetAsync(string movieCode);
        Task<PagedResultDto<movieDto>> GetlistAsync(PagedAndSortedResultRequestDto input);
        Task<movieDto> CreateAsync(IFormFile objFile ,string datajson);
        Task<movieDto> UpdateAsync(string movieCode, IFormFile objFile, string datajson);
        Task DeleteAsync(string movieCode);
    }
}
