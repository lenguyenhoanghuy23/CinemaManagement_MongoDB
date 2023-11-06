using CinemaManagement.MongoDB;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Newtonsoft.Json;
using Volo.Abp.Domain.Repositories;
using System.Linq;
using System.IO;
using MongoDB.Bson.Serialization;
using Volo.Abp;
using System.Runtime.Intrinsics.X86;
using System.Collections.Generic;
using Volo.Abp.ObjectMapping;
using System.Text.Json;
using CinemaManagement.Genres;
using CinemaManagement.Countries;
using CinemaManagement.CinemaZooms;
using CinemaManagement.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace CinemaManagement.Movies
{
    [Authorize(CinemaManagementPermissions.Movies.Default)]
    public class movieAppService : CinemaManagementAppService, IMovieAppService
    {
        private readonly CinemaManagementMongoDbContext _context;
        public movieAppService(CinemaManagementMongoDbContext context)
        {
            _context = context;
        }

        [Authorize(CinemaManagementPermissions.Movies.Create)]
        public async Task<movieDto> CreateAsync(IFormFile objFile, string datajson)
        {
            try
            {
                var movieJson = JsonConvert.DeserializeObject<movieCreateDto>(datajson);
                string avt = "";
                if (FindByCode(_context.Movies, "movieCode", movieJson.movieCode) == 0)
                {

                    var genreArray = new BsonArray(movieJson.genres.Select(x => x));
                    var countrieArray = new BsonArray(movieJson.countries.Select(x => x));

                    var bson = new BsonDocument
                    {
                        { "movieCode" , movieJson.movieCode },
                        { "movieName" , movieJson.movieName },
                        { "description" , movieJson.description },
                        { "releaseDate" , movieJson.releaseDate },
                        { "endDate" , movieJson.endDate },
                        { "runningTime" , movieJson.runningTime },
                        { "year" , movieJson.year },
                        { "producer" , movieJson.producer },
                        { "Actor" , movieJson.Actor },
                        { "Director" , movieJson.Director },
                        { "genres" , genreArray },
                        { "countries" , countrieArray },
                        { "avata" , await Image(objFile) },
                    };


                    var countrie = BsonSerializer.Deserialize<movie>(bson);
                    await _context.Movies.InsertOneAsync(countrie);
                    return await GetAsync(movieJson.movieCode);
                }
                throw new UserFriendlyException("movieCode " + movieJson.movieCode + " isExited");
            }
            catch (Exception)
            {

                throw;
            }


        }



        [Authorize(CinemaManagementPermissions.Movies.Delete)]

        public async Task DeleteAsync(string movieCode)
        {
            var movie = new BsonDocument("movieCode", movieCode);
            await _context.Movies.DeleteOneAsync(movie);
        }

        public async Task<movieDto> GetAsync(string movieCode)
        {
            var filter = new BsonDocument("movieCode", movieCode);
            var countrie = await _context.Movies.Find(filter).FirstOrDefaultAsync();

            return ObjectMapper.Map<movie, movieDto>(countrie);
        }

        public async Task<PagedResultDto<movieDto>> GetlistAsync(PagedAndSortedResultRequestDto input)
        {
            try
            {
                if (input.Sorting.IsNullOrWhiteSpace())
                {
                    input.Sorting = nameof(movie.movieName);
                }


                var filter = new BsonDocument();
                var total = await _context.Movies.Find(filter).ToListAsync();

                var result = await _context.Movies.Find(filter)
                  .Skip(input.SkipCount)
                  .Limit(input.MaxResultCount)
                  .ToListAsync();




                var movieDto = ObjectMapper.Map<List<movie>, List<movieDto>>(result);
                movieDto.Skip(input.SkipCount).Take(input.MaxResultCount);
                var genreQueryable = await _context.Genres.Find(filter).ToListAsync();
                var countrieQueryable = await _context.Countries.Find(filter).ToListAsync();
                var genreDict = await CreateGenreDictionaryAsync(result, genreQueryable);
                movieDto.ForEach(movie =>
                {
                    movie.genreName = movie.genres
                        .Select(genreCode => genreDict.TryGetValue(genreCode, out var genreName) ? genreName : null)
                        .ToArray();
                });

                var countrieDict = await CreateCountrieDictionaryAsync(result, countrieQueryable);

                movieDto.ForEach(movie =>
                {
                    movie.countrieName = movie.countries
                        .Select(countrieId => countrieDict.TryGetValue(countrieId, out var countrieName) ? countrieName : null)
                        .ToArray();
                });

                return new PagedResultDto<movieDto>(
                    total.Count(),
                    movieDto
                ); ;

            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize(CinemaManagementPermissions.Movies.Edit)]
        public async Task<movieDto> UpdateAsync(string movieCode, IFormFile objFile, string datajson)
        {
            var movieJson = JsonConvert.DeserializeObject<movieUpdateDto>(datajson);
            if ( FindByCode(_context.Movies, "movieCode", movieCode) != 0)
            {
                var genreArray = new BsonArray(movieJson.genres.Select(x => x));
                var countrieArray = new BsonArray(movieJson.countries.Select(x => x));
                var filter = new BsonDocument("movieCode", movieCode);
                var update = new BsonDocument("$set",
                    new BsonDocument
                    {
                        { "movieName" , movieJson.movieName },
                        { "description" , movieJson.description },
                        { "releaseDate" , movieJson.releaseDate },
                        { "endDate" , movieJson.endDate },
                        { "runningTime" , movieJson.runningTime },
                        { "year" , movieJson.year },
                        { "producer" , movieJson.producer },
                        { "Actor" , movieJson.Actor },
                        { "Director" , movieJson.Director },
                        { "genres" , genreArray },
                        { "countries" , countrieArray },
                        { "avata" , await Image(objFile) },
                    }
                );
                await _context.Movies.UpdateOneAsync(filter, update);
                return await GetAsync(movieCode);

            }
            throw new UserFriendlyException("movieCode " + movieCode + " dose not exited");
        }


        private async Task<string> Image(IFormFile objFile)
        {

            if (objFile.Length > 0)
            {
                try
                {

                    using (var stream = System.IO.File.Create(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", objFile.FileName)))
                    {
                        await objFile.CopyToAsync(stream);
                    }
                    return "/images/" + objFile.FileName;
                }
                catch (Exception)
                {

                    throw;
                }
            }


            return "";

        }


        private async Task<Dictionary<string, string>> CreateGenreDictionaryAsync(List<movie> movies, List<genre> genreQueryable)
        {
            var IDs = movies
                .SelectMany(movie => movie.genres)
                .Distinct()
                .ToList();

            var Dict = genreQueryable
                .Where(c => IDs.Contains(c.genreCode))
                .Select(c => new { c.genreCode, c.genreName })
                .ToDictionary(c => c.genreCode, c => c.genreName);

            return Dict;
        }
        private async Task<Dictionary<string, string>> CreateCountrieDictionaryAsync(List<movie> movies, List<countrie> genreQueryable)
        {
            var IDs = movies
                .SelectMany(movie => movie.countries)
                .Distinct()
                .ToList();

            var Dict = genreQueryable
                .Where(c => IDs.Contains(c.countrieCode))
                .Select(c => new { c.countrieCode, c.countrieName })
                .ToDictionary(c => c.countrieCode, c => c.countrieName);

            return Dict;
        }
    }
}
