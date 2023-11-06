
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace CinemaManagement.Genres;

public interface IGenreAppService
{
    Task<genreDto> GetAsync(string genreCode);
    Task<PagedResultDto<genreDto>> GetlistAsync(PagedAndSortedResultRequestDto input);
    Task<genreDto> CreateAsync(genreCreateDto input);
    Task<genreDto> UpdateAsync(string genreCode, genreUpdateDto input);
    Task DeleteAsync(string genreCode);
}
