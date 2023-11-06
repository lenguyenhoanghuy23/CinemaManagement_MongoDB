using CinemaManagement.MongoDB;
using CinemaManagement.Movies;
using CinemaManagement.Permissions;
using CinemaManagement.ShowTimes;
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
using Volo.Abp.ObjectMapping;

namespace CinemaManagement.Tickets
{
    [Authorize(CinemaManagementPermissions.ShowTimes.Default)]
    public class tickerAppService : CinemaManagementAppService, ITicketAppService
    {
        private readonly CinemaManagementMongoDbContext _context;
        public tickerAppService(CinemaManagementMongoDbContext context)
        {
            _context = context;
        }

        [Authorize(CinemaManagementPermissions.ShowTimes.Create)]
        public async Task<ticketDto> CreateAsync(string showTimeCode)
        {
            if (FindByCode(_context.ShowTimes, "showTimeCode", showTimeCode) == 0)
            {
                throw new UserFriendlyException("showTimeCode " + showTimeCode + " dose not exited");
            }
            var filter = new BsonDocument("showTimeCode", showTimeCode);
            var showtime = await _context.ShowTimes.Find(filter).FirstOrDefaultAsync();
            if (showtime != null)
            {
                if (showtime.status == 1)
                {
                    throw new UserFriendlyException("LỊCH CHIẾU NÀY ĐÃ ĐƯỢC TẠO VÉ!!!");
                }
                AutoCreateTicketsByShowTimes(showTimeCode, showtime.cinemaZoom);
                var updateStatus = new BsonDocument("$set", new BsonDocument("status", 1));
                await _context.ShowTimes.UpdateOneAsync(filter, updateStatus);
                throw new UserFriendlyException("TẠO VÉ TỰ ĐỘNG THÀNH CÔNG!");
            }

            throw new UserFriendlyException("TẠO VÉ TỰ ĐỘNG THẤT BẠI!");
        }

        [Authorize(CinemaManagementPermissions.ShowTimes.Delete)]
        public async Task DeleteAsync(string showTimeCode)
        {

            try
            {
                // filter để lấy ra tông tin showitme trong ticket rồi xóa toàn bộ vé có mã showtime được chọn
                var filterShowTime_ticket = new BsonDocument("showtime", showTimeCode);
                var ticket = await _context.Tickets.Find(filterShowTime_ticket).FirstOrDefaultAsync();
                if (ticket != null)
                {
                    // filter lấy ra showtimeCode update lại status =0 showtime sau khi xóa 
                    var filterShowTime = new BsonDocument("showTimeCode", showTimeCode);
                    var updateStatus = new BsonDocument("$set", new BsonDocument("status", 0));
                    var showtime = await _context.ShowTimes.Find(filterShowTime).FirstOrDefaultAsync();

                    if (ticketPurchased(showTimeCode) == true)
                    {
                        throw new UserFriendlyException("Lỗi! vé đã được bán !!!");
                    }
                    if (showtime.status == 0)
                    {
                        throw new UserFriendlyException("LỊCH CHIẾU NÀY CHƯA ĐƯỢC TẠO VÉ !!!");
                    }
                    await _context.Tickets.DeleteManyAsync(filterShowTime_ticket); // xóa toàn bộ vé có mã showtime được chọn
                    await _context.ShowTimes.UpdateOneAsync(filterShowTime, updateStatus);
                    throw new UserFriendlyException($"XÓA TẤT CẢ CÁC VÉ CỦA LỊCH CHIẾU Mã=\" +{showtime}+ \" THÀNH CÔNG!\"");



                }

            }
            catch (Exception)
            {

                throw;
            }
        }



        public Task<PagedResultDto<ticketDto>> GetlistAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        //public Task<ticketDto> UpdateAsync(string showTime, ticketUpdateDto input)
        //{
        //    throw new NotImplementedException();
        //}

        private void AutoCreateTicketsByShowTimes(string showtimeCode, string cinemaZoom)
        {
            try
            {
                int seatTotal = 0;
                var findStage = new BsonDocument("cinemaCode", cinemaZoom);
                var cinema = _context.CinemaZooms.Find(findStage).FirstOrDefault();
                int row = cinema.rowSeats;
                int columns = cinema.columnSeats;
                for (int i = 0; i < row; i++)
                {
                    char seatCode = (char)('A' + i);
                    for (int j = 1; j <= columns; j++)
                    {
                        string seatName = $"{seatCode}{j}";
                        var bson = new BsonDocument
                        {
                            {"showtime",showtimeCode },
                            {"seatcode" , seatName}
                        };
                        seatTotal++;
                        var ticker = BsonSerializer.Deserialize<ticket>(bson);
                        _context.Tickets.InsertOne(ticker);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new UserFriendlyException(ex.InnerException!.Message ?? ex.Message);
            }
        }

        private bool ticketPurchased(string showTimeCode)
        {
            var count_Status_gt_0 = new List<BsonDocument>
                {
                    new BsonDocument("$match",new BsonDocument
                    {
                        {"status",1 },
                        {"showtime",showTimeCode }
                    }),
                    new BsonDocument("$group", new BsonDocument
                    {
                        { "_id", BsonNull.Value },
                        { "count", new BsonDocument("$sum", 1) }
                    })
                };
            var aggregationResult = _context.Tickets.Aggregate<BsonDocument>(count_Status_gt_0).ToList();

            if (aggregationResult.Count > 0)
            {
                return true; // đã có vé được bán
            }
            return false; // chưa có vé được bán
        }

        private bool ticketCreated(int status)
        {

            if (status == 0)
            {
                return true;
            }
            return false;
        }

        public async Task<PagedResultDto<ticketDto>> GetAsync(string showtimeCode)
        {
            var bsonFilter = new BsonDocument("showtime", showtimeCode);
            var ticket = await _context.Tickets.Find(bsonFilter).ToListAsync();
            var totalCount = ticket.Count;

            var infoShowtime = await _context.ShowTimes.Find(bsonFilter).FirstOrDefaultAsync();

            var ticketDto = ObjectMapper.Map<List<ticket>, List<ticketDto>>(ticket);
            ticketDto.ForEach(ticket => { });

            ticketDto.ForEach(x => x.price  = showtimeinfo(showtimeCode).price);

            return new PagedResultDto<ticketDto>(
                   totalCount,
                   ticketDto

            );
        }


        private showTime showtimeinfo(string showTimeCode)
        {
            var bson = new BsonDocument("showTimeCode", showTimeCode);
            var showtime = _context.ShowTimes.Find(bson).FirstOrDefault();
            return showtime;

        }
    }
}
