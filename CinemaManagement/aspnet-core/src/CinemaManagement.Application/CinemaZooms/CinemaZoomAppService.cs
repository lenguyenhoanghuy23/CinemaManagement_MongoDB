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


namespace CinemaManagement.CinemaZooms
{
    [Authorize(CinemaManagementPermissions.CinemaZooms.Default)]
    public class CinemaZoomAppService : CinemaManagementAppService, ICinemaZoomAppService
    {
        private readonly CinemaManagementMongoDbContext _context;
        public CinemaZoomAppService(CinemaManagementMongoDbContext context)
        {
            _context = context;
        }

        [Authorize(CinemaManagementPermissions.CinemaZooms.Create)]
        public async Task<cinemaZoomDto> CreateAsync(cinemaZoonCreateDto input)
        {
            try
            {
               
                if (FindByCode(_context.CinemaZooms, "cinemaCode", input.cinemaCode) == 0) //FindByCode :  hỏi có mã được truyền vào không  == 0 mới dc tạo
                {
                    var bson = new BsonDocument
                    {
                        { "cinemaCode", input.cinemaCode.ToUpper() },
                        { "cinemaName", input.cinemaName },
                        { "numberSeats", input.numberSeats },
                        { "rowSeats", input.rowSeats },
                        { "columnSeats", input.columnSeats },
                        { "status", input.status },
                       

                    };

                    var cinema = BsonSerializer.Deserialize<cinemaZoom>(bson);
                    await _context.CinemaZooms.InsertOneAsync(cinema);
                    return await GetAsync(input.cinemaCode);


                }

                throw new UserFriendlyException("cinemaCode " + input.cinemaCode + " ísExisted");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(CinemaManagementPermissions.CinemaZooms.Delete)]
        public async Task DeleteAsync(string cinemaCode)
        {
            var filter = new BsonDocument("cinemaCode", cinemaCode);
            // Thực hiện truy vấn xóa
            await _context.CinemaZooms.DeleteOneAsync(filter);
        }

        public async Task<cinemaZoomDto> GetAsync(string cinemaCode)
        {
            var filter = new BsonDocument("cinemaCode", cinemaCode); // truy vấn có tham số ở đây truyền tham số cinemaCode 
            var cinema = await _context.CinemaZooms.Find(filter).FirstOrDefaultAsync(); // find có tham số 

            return ObjectMapper.Map<cinemaZoom, cinemaZoomDto>(cinema);
        }

        public async Task<PagedResultDto<cinemaZoomDto>> GetlistAsync(PagedAndSortedResultRequestDto input)
        {
            try
            {
                if (input.Sorting.IsNullOrWhiteSpace())
                {
                    input.Sorting = nameof(cinemaZoom.cinemaName);
                }


                var filter = new BsonDocument(); // tìm toàn document có trong db do là lấy toàn bộ nên không cần truyền tham số
                var countries = await _context.CinemaZooms.Find(filter).ToListAsync(); // query find monpgo truyền vào filter rỗng để lấy toàn bộ dữ liệu
                var totalCount = countries.Count(); // count đếm trong collection có bao nhiêu đóc 

                var result = await _context.CinemaZooms.Find(filter)
                  .Skip(input.SkipCount)
                  .Limit(input.MaxResultCount)
                  .ToListAsync();
                return new PagedResultDto<cinemaZoomDto>(
                    totalCount,
                     ObjectMapper.Map<List<cinemaZoom>, List<cinemaZoomDto>>(result)
                ); ;

            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(CinemaManagementPermissions.CinemaZooms.Edit)]
        public async Task<cinemaZoomDto> UpdateAsync(string cinemaCode, cinemaZoomUpdateDto input)
        {

         
            if (FindByCode(_context.CinemaZooms, "cinemaCode", cinemaCode) != 0)
            {
                var filter = new BsonDocument("cinemaCode", cinemaCode);
                var update = new BsonDocument("$set", new BsonDocument
                {
                    { "cinemaName", input.cinemaName },
                    { "numberSeats", input.numberSeats },
                    { "rowSeats", input.rowSeats },
                    { "columnSeats", input.columnSeats },
                    { "status", input.status },
                
                });

                await _context.CinemaZooms.UpdateOneAsync(filter, update);
                return await GetAsync(cinemaCode);
            }

            throw new UserFriendlyException("Cinema with code " + cinemaCode + " does not exist");
        }
    }
}

