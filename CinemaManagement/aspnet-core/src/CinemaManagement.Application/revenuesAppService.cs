using CinemaManagement.MongoDB;
using CinemaManagement.revenues;
using CinemaManagement.Tickets;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace CinemaManagement
{
    public class revenuesAppService : ApplicationService, IRevenuesAppservice
    {
        private readonly CinemaManagementMongoDbContext _context;
        public revenuesAppService(CinemaManagementMongoDbContext context)
        {
            _context = context;
        }

        public float totalCinemaCost()
        {
            var filter = new BsonDocument();
            var group = new BsonDocument
            {
                {
                    "$group", new BsonDocument
                    {
                        { "_id", BsonNull.Value },
                        { "total", new BsonDocument { { "$sum", "$priceticker" } } }
                    }
                }
            };

            var pipeline = new BsonDocument[] { group };

            var result = _context.Tickets.Aggregate<BsonDocument>(pipeline).FirstOrDefault();
           

            if (result != null)
            {
                var total = result.GetValue("total").AsDouble;
                return (float)total;
            }

            return 0.0f;

        }

        public float Totalticketssoldperweek()
        {
            throw new NotImplementedException();
        }
    }
}
