using CinemaManagement.CinemaZooms;
using CinemaManagement.MongoDB;
using CinemaManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CinemaManagement.Genres
{
    [Authorize(CinemaManagementPermissions.Genres.Default)]
    public class genreAppService : ApplicationService, IGenreAppService
    {
        private readonly CinemaManagementMongoDbContext _context;

        public genreAppService(
            CinemaManagementMongoDbContext context

           )
        {
            _context = context;

        }

        [Authorize(CinemaManagementPermissions.Genres.Create)]
        public async Task<genreDto> CreateAsync(genreCreateDto input)
        {

            try
            {
               
                if (FindByCode(input.genreCode) == 0)
                {
                    var bson = new BsonDocument
                    {
                       {"genreCode",input.genreCode.ToUpper() },
                       {"genreName",input.genreName }
                    };
                    var genre = BsonSerializer.Deserialize<genre>(bson);
                    await _context.Genres.InsertOneAsync(genre);
                    return await GetAsync(input.genreCode);

                }
            }
            catch (Exception)
            {

                throw;
            }
            throw new UserFriendlyException("genreCode " + input.genreCode + " isExited");
        }

        [Authorize(CinemaManagementPermissions.Genres.Delete)]
        public async Task DeleteAsync(string genreCode)
        {
            var bson = new BsonDocument("genreCode", genreCode);
            await _context.Genres.DeleteOneAsync(bson);
        }

        public async Task<genreDto> GetAsync(string genreCode)
        {
            var filter = new BsonDocument("genreCode", genreCode);
            var countrie = await _context.Genres.Find(filter).FirstOrDefaultAsync();
            return ObjectMapper.Map<genre, genreDto>(countrie);
        }

        public async Task<PagedResultDto<genreDto>> GetlistAsync(PagedAndSortedResultRequestDto input)
        {
            try
            {
                if (input.Sorting.IsNullOrWhiteSpace())
                {
                    input.Sorting = nameof(genre.genreName);
                }

                var filter = new BsonDocument();
                var countries = await _context.Genres.Find(filter).ToListAsync();
                var totalCount = countries.Count();
                var result = await _context.Genres.Find(filter)
                  .Skip(input.SkipCount)
                  .Limit(input.MaxResultCount)
             
                  .ToListAsync();

                return new PagedResultDto<genreDto>(
                    totalCount,
                     ObjectMapper.Map<List<genre>, List<genreDto>>(result)
                ); ;

                
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [Authorize(CinemaManagementPermissions.Genres.Edit)]
        public async Task<genreDto> UpdateAsync(string genreCode, genreUpdateDto input)
        {
            if (FindByCode(genreCode) != 0)
            {
                var filter = new BsonDocument("genreCode", genreCode);
                var update = new BsonDocument("$set", new BsonDocument("genreName", input.genreName));
                await _context.Genres.UpdateOneAsync(filter, update);
                return await GetAsync(genreCode);
            }
            throw new UserFriendlyException("genreCode " + genreCode + " dose not exited");
        }

        private long FindByCode(string code)
        {
            var Exited = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("genreCode", code)),
            };

            var aggregateResult = _context.Genres.Aggregate<BsonDocument>(Exited).FirstOrDefault();

            if (aggregateResult != null)
            {
                return 1;
            }

            return 0;
        }
    }
}
