using CinemaManagement.CinemaZooms;
using CinemaManagement.Countries;
using CinemaManagement.MongoDB;
using CinemaManagement.Movies;
using CinemaManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace CinemaManagement.ShowTimes
{
    [Authorize(CinemaManagementPermissions.ShowTimes.Default)]
    public class showTimeAppService : CinemaManagementAppService, IShowTimeAppsService
    {
        private readonly CinemaManagementMongoDbContext _context;
        public showTimeAppService(CinemaManagementMongoDbContext context)
        {
            _context = context;
        }
        public async Task<showTimeDto> CreateAsync(showTimeCreateDto input)
        {
            if (DuplicateSchedule(input.movieSchedule, input.cinemaZoom) == true)
            {
                throw new UserFriendlyException("ShowTimes đã bị trùng lặp với một lịch trình khác trong cùng Phòng");
            }

            if (CheckTimeDifferenceBetweenShowtimes(input.movieSchedule,  input.cinemaZoom, TimeSpan.FromMinutes(45)) == true)
            {
                throw new UserFriendlyException("Không thể tạo buổi chiếu quá gần với buổi chiếu trước đó.");
            }
            if (CheckShowtimeInMovieTimeRange(input.movieSchedule, input.movie) == true)
            {
                throw new UserFriendlyException("Buổi chiếu không nằm trong khoảng thời gian hợp lệ của bộ phim. " +
                                               "Vui lòng đảm bảo thời gian buổi chiếu lớn hơn ngày khởi chiếu và bé hơn ngày kết thúc.");
            }

            if (FindByCode(_context.ShowTimes, "showTimeCode", input.showTimeCode) == 0)
            {
                var bson = new BsonDocument
                {
                    { "showTimeCode",input.showTimeCode.ToUpper() },
                    { "movieSchedule",input.movieSchedule },
                    { "price",input.price },
                    { "status",0 },
                    { "movie",input.movie },
                    { "screenType",input.screenType },
                    { "cinemaZoom",input.cinemaZoom },
                };

                var showtime = BsonSerializer.Deserialize<showTime>(bson);
                await _context.ShowTimes.InsertOneAsync(showtime);
                return await GetAsync(input.showTimeCode);
            }
            throw new UserFriendlyException("showTimeCode " + input.showTimeCode + " ísExisted");
        }

        [Authorize(CinemaManagementPermissions.ShowTimes.Delete)]
        public async Task DeleteAsync(string showtimeCode)
        {

            var bson = new BsonDocument("showTimeCode", showtimeCode);
            var Stime = _context.ShowTimes.Find(bson).FirstOrDefault();
            if (Stime.status == 0)
            {
                await _context.ShowTimes.DeleteOneAsync(bson);
                return;
            }
            throw new UserFriendlyException("Lỗi! vé đã được tạo !!!");
        }

        public async Task<showTimeDto> GetAsync(string showtimeCode)
        {
            var filter = new BsonDocument("showTimeCode", showtimeCode);
            var showTime = await _context.ShowTimes.Find(filter).FirstOrDefaultAsync();

            return ObjectMapper.Map<showTime, showTimeDto>(showTime);
        }

        public async Task<PagedResultDto<showTimeDto>> GetlistAsync(PagedAndSortedResultRequestDto input)
        {
            try
            {
                var filter = new BsonDocument();
                var showtime = await _context.ShowTimes.Find(filter).ToListAsync();
                var totalCount = showtime.Count;
                var result = await _context.ShowTimes.Find(filter)
                  .Skip(input.SkipCount)
                  .Limit(input.MaxResultCount)
                  .Sort(input.Sorting)
                  .ToListAsync();

                var showtimeDto = ObjectMapper.Map<List<showTime>, List<showTimeDto>>(result);

                showtimeDto.ForEach(x => x.movieAvata = movieinfo(x.movie).avata);
                showtimeDto.ForEach(x => x.movieName = movieinfo(x.movie).movieName);

                return new PagedResultDto<showTimeDto>(
                    totalCount,
                   showtimeDto

                );

            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(CinemaManagementPermissions.ShowTimes.Edit)]
        public async Task<showTimeDto> UpdateAsync(string showtimeCode, showTimeUpdateDto input)
        {

            if (ScheduleExited(input.movieSchedule , showtimeCode) ==true)
            {
                if (CheckTimeDifferenceBetweenShowtimes(input.movieSchedule, input.cinemaZoom, TimeSpan.FromMinutes(45)) == true)
                {
                    throw new UserFriendlyException("Không thể tạo buổi chiếu quá gần với buổi chiếu trước đó.");
                }

                if (CheckShowtimeInMovieTimeRange(input.movieSchedule, input.movie) == true)
                {
                    throw new UserFriendlyException("Buổi chiếu không nằm trong khoảng thời gian hợp lệ của bộ phim. " +
                                                   "Vui lòng đảm bảo thời gian buổi chiếu lớn hơn ngày khởi chiếu và bé hơn ngày kết thúc.");
                }
                if (DuplicateSchedule(input.movieSchedule, input.cinemaZoom) == true)
                {
                    throw new UserFriendlyException("ShowTimes đã bị trùng lặp với một lịch trình khác trong cùng Phòng");
                }
            }

            if (FindByCode(_context.ShowTimes, "showTimeCode", showtimeCode) != 0)
            {
                var filter = new BsonDocument("showTimeCode", showtimeCode);
                var update = new BsonDocument("$set", new BsonDocument
                {
                    { "movieSchedule",input.movieSchedule },
                    { "price",input.price },
                    { "movie",input.movie },
                    { "screenType",input.screenType },
                    { "cinemaZoom",input.cinemaZoom },
                });
                await _context.ShowTimes.UpdateOneAsync(filter, update);
                return await GetAsync(showtimeCode);

            }
            throw new UserFriendlyException("showTimeCode " + showtimeCode + " dose not exited");
        }


        private bool DuplicateSchedule(DateTime movieSchedule, string cinemaZoom)
        {

            var matchStage = new BsonDocument("$match", new BsonDocument // match theo 2 field movieSchedule,cinemaZoom
            {
                { "movieSchedule", movieSchedule },
                { "cinemaZoom", cinemaZoom }
            });
            var pipeline = new List<BsonDocument> { matchStage }; // trả về bsondocument

            var aggregation = _context.ShowTimes.Aggregate<BsonDocument>(pipeline);

            var results = aggregation.ToList();

            if (results.Count > 0) // kiểm tra xem results có giá trị không nếu >0 là có giá trị ngược lại là không có giá trị nào trùng trong db cả 
            {
                return true; // nếu có tồn tại trong db thì trả về true
            }

            return false; // trả về false
        }

        private bool CheckShowtimeInMovieTimeRange(DateTime movieSchedule, string moviecode)
        {

            var filter = new BsonDocument("movieCode", moviecode); // filter theo moviecode 
            var movie = _context.Movies.Find(filter).FirstOrDefault();
            DateTime releaseDate = movie.releaseDate;
            DateTime endDate = movie.endDate;

            if (movieSchedule < releaseDate || movieSchedule > endDate) // kiểm tra xem có hợp lệ không
            {
                return true;
            }
            return false;

        }
        private bool CheckTimeDifferenceBetweenShowtimes(DateTime newShowtime ,string cinemaCode, TimeSpan minTimeDifference)
        {

            

            var matchStage = new BsonDocument("$match", new BsonDocument // mactch giá trị cinemaZoom 
            {
                { "cinemaZoom", cinemaCode }
            });

            var sortStage = new BsonDocument("$sort", new BsonDocument // Sắp xếp theo _id giảm dần (Descending)
            {
                { "_id", -1 }
            });
            var limitStage = new BsonDocument("$limit", 1); // // Lấy kết quả đầu tiên sau khi đã sắp xếp

            var pipeline = new List<BsonDocument> { matchStage, sortStage, limitStage };

            var aggregation = _context.ShowTimes.Aggregate<showTime>(pipeline).FirstOrDefault();

            if (aggregation == null)
            {
                return false; // databas chưa có dữ liệu để lọc nên trả về false luôn không cần xét điều kiện
            }
            var movieRuntime = movieinfo(aggregation.movie).runningTime;

        

            var timeDifference = timeConvert( newShowtime)- (timeConvert(aggregation.movieSchedule) + TimeSpan.FromMinutes(movieRuntime)); // nếu timeDifference bé hơn 45p thì thông báo lịch tiếp theo phải lớn hơn thời gian của phòng hiện tại 

            if (timeDifference <minTimeDifference)
            {
                return true; //  trả về true yêu cầu khoảng time giữa các rạp tối thiểu 45p
            }

            return false; // thỏa mãn điều kiện trên
        }

        private bool ScheduleExited(DateTime scheduleNew ,string showtimeCode)
        {
            var bson = new BsonDocument("movieSchedule",
                new BsonDocument("$eq", scheduleNew)
            ) ;
            var showtime = _context.ShowTimes.Find(bson).FirstOrDefault();

            if (showtime == null)
            {
                return true; // bằng true không có lịch 
            }

            return false;
            
        }

        private  movie movieinfo(string movieCode)
        {
            var bson = new BsonDocument("movieCode", movieCode);
            var movie =  _context.Movies.Find(bson).FirstOrDefault();
            return movie;

        }
        

    }
}
