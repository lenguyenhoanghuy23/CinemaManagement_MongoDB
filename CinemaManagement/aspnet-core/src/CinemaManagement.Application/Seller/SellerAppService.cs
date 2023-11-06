using CinemaManagement.MongoDB;
using CinemaManagement.Movies;
using CinemaManagement.Permissions;
using CinemaManagement.ShowTimes;
using CinemaManagement.Tickets;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace CinemaManagement.Seller
{
    [Authorize(CinemaManagementPermissions.Sellers.Default)]
    public class SellerAppService : CinemaManagementAppService, ISellerAppService
    {
        private readonly CinemaManagementMongoDbContext _context;
        public SellerAppService(CinemaManagementMongoDbContext context) 
        { 
            _context = context;
        }

   
        public async Task<PagedResultDto<showTimeDto>> GetlistSchedulerAsync(DateTime schedule)
        {
            var matchStage = new BsonDocument("$match", new BsonDocument("date",  schedule.ToString("yyyy-MM-dd")));

            var projectStage = new BsonDocument("$project", new BsonDocument
            {
                { "_id", 0 },
                { "showTimeCode", 1 },
                { "movieSchedule", 1 },
                { "status", 1 },
                { "cinemaZoom", 1 },
                { "movie", 1 },
                { "screenType", 1 },
                { "price", 1 },
            });

            var addFieldsStage = new BsonDocument("$addFields", new BsonDocument
            {
                { "date", new BsonDocument
                    {
                        { "$dateToString", new BsonDocument
                            {
                                { "format", "%Y-%m-%d" },
                                { "date", "$movieSchedule" }
                            }
                        }
                    }
                }
            });

            var pipeline = new List<BsonDocument> { addFieldsStage, matchStage, projectStage }; 

            var aggregation = await _context.ShowTimes.Aggregate<showTime>(pipeline).ToListAsync();
         
            var showtimeDto = ObjectMapper.Map<List<showTime>, List<showTimeDto>>(aggregation);

            showtimeDto.ForEach(x => x.movieAvata = movieinfo(x.movie).avata);
            showtimeDto.ForEach(x => x.movieName = movieinfo(x.movie).movieName);

            return new PagedResultDto<showTimeDto>(
                showtimeDto.Count,
                showtimeDto
            );
        }

      

        public async Task updateAsync(List<ticketUpdateDto> input)
        {
            
            foreach (var item in input)
            {
                var filter = new BsonDocument
                {
                    { "showtime", item.showtime },
                    { "seatcode", item.seatsCode },
                };
                var update = new BsonDocument
                {
                    { "$set", new BsonDocument
                        {
                            { "priceticker", item.priceticker },
                            { "status", 1 }
                        }
                    }
                };
                await _context.Tickets.UpdateOneAsync(filter , update);
            }
        }
        
        private movie movieinfo(string movieCode)
        {
            var bson = new BsonDocument("movieCode", movieCode);
            var movie = _context.Movies.Find(bson).FirstOrDefault();
            return movie;
        }

        
    }
}
