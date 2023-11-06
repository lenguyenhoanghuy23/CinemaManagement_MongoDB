
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
using Volo.Abp.ObjectMapping;

namespace CinemaManagement.Countries
{

    [Authorize(CinemaManagementPermissions.Countries.Default)]
    public class CountrieAppService : ApplicationService, ICountrieAppService
    {
        private readonly CinemaManagementMongoDbContext _context;
        public CountrieAppService(
            CinemaManagementMongoDbContext mongoDbContext
            )
        {
            _context = mongoDbContext;
        }

        [Authorize(CinemaManagementPermissions.Countries.Create)]
        public async Task<countrieDto> CreateAsync(countrieCreateDto input)
        {
            try
            {
                if (FindByCode(input.countrieCode) == 0)
                {
                    var bson = new BsonDocument
                    {
                       {"countrieCode",input.countrieCode.ToUpper() },
                       {"countrieName",input.countrieName }
                    };


                    var countrie = BsonSerializer.Deserialize<countrie>(bson);
                    await _context.Countries.InsertOneAsync(countrie);
                    return await GetAsync(input.countrieCode);

                }
            }
            catch (Exception)
            {

                throw;
            }
            throw new UserFriendlyException("countrieCode " + input.countrieCode + " ísExisted");
        }

        [Authorize(CinemaManagementPermissions.Countries.Delete)]
        public async Task DeleteAsync(string countrieCode)
        {
            var countrie = new BsonDocument("countrieCode", countrieCode);
            await _context.Countries.DeleteOneAsync(countrie);
        }

        public async Task<countrieDto> GetAsync(string countrieCode)
        {
            var filter = new BsonDocument("countrieCode", countrieCode);
            var countrie = await _context.Countries.Find(filter).FirstOrDefaultAsync();

            return ObjectMapper.Map<countrie, countrieDto>(countrie);
        }


        public async Task<PagedResultDto<countrieDto>> GetlistAsync(PagedAndSortedResultRequestDto input)
        {

            try
            {
                if (input.Sorting.IsNullOrWhiteSpace())
                {
                    input.Sorting = nameof(countrie.countrieName);
                }
                var filter = new BsonDocument();
                var countries = await _context.Countries.Find(filter).ToListAsync();
                var totalCount = countries.Count();
                var result = await _context.Countries.Find(filter)
                  .Skip(input.SkipCount)
                  .Limit(input.MaxResultCount)
                  .ToListAsync();

                return new PagedResultDto<countrieDto>(
                    totalCount,
                     ObjectMapper.Map<List<countrie>, List<countrieDto>>(result)
                );

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [Authorize(CinemaManagementPermissions.Countries.Edit)]
        public async Task<countrieDto> UpdateAsync(string countrieCode, countrieUpdateDto input)
        {
            if (FindByCode(countrieCode) != 0)
            {
                var filter = new BsonDocument("countrieCode", countrieCode);
                var update = new BsonDocument("$set", new BsonDocument("countrieName", input.countrieName));
                await _context.Countries.UpdateOneAsync(filter, update);
                return await GetAsync(countrieCode);
            }
            throw new UserFriendlyException("countrieCode " + countrieCode + " dose not exited");
            
        }

        private long FindByCode(string countrieCode)
        {
            var Exited = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("countrieCode", countrieCode)),
            };

            var aggregateResult = _context.Countries.Aggregate<BsonDocument>(Exited).FirstOrDefault();

            if (aggregateResult != null)
            {
                return 1;
            }

            return 0;
        }
    }
}
