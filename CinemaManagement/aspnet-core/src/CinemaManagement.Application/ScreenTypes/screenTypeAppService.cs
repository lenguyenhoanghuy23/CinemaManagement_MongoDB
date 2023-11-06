using CinemaManagement.MongoDB;
using CinemaManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;


namespace CinemaManagement.ScreenTypes
{
    [Authorize(CinemaManagementPermissions.screenTypes.Default)]
    public class screenTypeAppService : ApplicationService, IScreenTypeAppService
    {
        private readonly CinemaManagementMongoDbContext _context;
        public screenTypeAppService(CinemaManagementMongoDbContext mongoDbContext)
        {
            // Tạo kết nối đến MongoDB

            _context = mongoDbContext;
        }

        [Authorize(CinemaManagementPermissions.screenTypes.Create)]
        public async Task<screenTypeDto> CreateAsync(screenTypeCreateDto input)
        {
            try
            {
                if (FindByCode(input.screenCode) == 0)
                {
                    var screendoc = new BsonDocument
                    {
                       {"screenCode",input.screenCode },
                       {"screenName",input.screenName }
                    };
                    var screenType = BsonSerializer.Deserialize<screenType>(screendoc);
                    await _context.ScreenTypes.InsertOneAsync(screenType);
                    //var result = ObjectMapper.Map<screenType, screenTypeDto>(screenType);
                    return await GetAsync(input.screenCode);

                }
            }
            catch (Exception)
            {

                throw;
            }
            throw new UserFriendlyException("screenCode " + input.screenCode + " isExited");
        }

        [Authorize(CinemaManagementPermissions.screenTypes.Delete)]
        public async Task DeleteAsync(string screenCode)
        {
            if (FindByCode(screenCode) == 1)
            {
                var filter = new BsonDocument("screenCode", screenCode);
                await _context.ScreenTypes.DeleteOneAsync(filter);
            }
        }

        public async Task<screenTypeDto> GetAsync(string screenCode)
        {

            var filter = new BsonDocument("screenCode", screenCode);

            // Thực hiện truy vấn find với điều kiện
            var screen = await _context.ScreenTypes.Find(filter).FirstOrDefaultAsync();

            return ObjectMapper.Map<screenType, screenTypeDto>(screen);
        }

        public async Task<PagedResultDto<screenTypeDto>> GetlistAsync(PagedAndSortedResultRequestDto input)
        {
            
            var filter = new BsonDocument();
            // Thực hiện truy vấn MongoDB để lấy danh sách genres
            var screens = await _context.ScreenTypes.Find(filter).ToListAsync();

            // Số lượng doc trong danh sách genres
            var totalCount = screens.Count;

            var result = await _context.ScreenTypes.Find(filter)
           .Skip(input.SkipCount)
           .Limit(input.MaxResultCount)
           .Sort(input.Sorting)
           .ToListAsync();

            return new PagedResultDto<screenTypeDto>(
                totalCount,
                ObjectMapper.Map<List<screenType>, List<screenTypeDto>>(result)
            );
        }


        [Authorize(CinemaManagementPermissions.screenTypes.Edit)]
        public async Task<screenTypeDto> UpdateAsync(string screenCode, screenTypeUpdateDto input)
        {
            if (FindByCode(screenCode) != 0)
            {
                var filter = new BsonDocument("screenCode", new BsonString(screenCode));
                var update = new BsonDocument("$set", new BsonDocument("screenName", input.screenName));
                await _context.ScreenTypes.UpdateOneAsync(filter, update);
                return await GetAsync(screenCode);
            }
            throw new UserFriendlyException("genreCode " + screenCode + " dose not exited");
        }


        private long FindByCode(string screenCode)
        {
            var screenTypeExited = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("screenCode", screenCode)),
            };

            var aggregateResult = _context.ScreenTypes.Aggregate<BsonDocument>(screenTypeExited).FirstOrDefault();

            if (aggregateResult != null)
            {
                return 1;
            }

            return 0;
        }

    }
}
