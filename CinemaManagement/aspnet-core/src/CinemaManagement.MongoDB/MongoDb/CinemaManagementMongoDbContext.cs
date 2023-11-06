using CinemaManagement.CinemaZooms;
using CinemaManagement.Countries;
using CinemaManagement.Genres;
using CinemaManagement.Movies;
using CinemaManagement.ScreenTypes;
using CinemaManagement.ShowTimes;
using CinemaManagement.Tickets;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace CinemaManagement.MongoDB;

[ConnectionStringName("Default")]
public class CinemaManagementMongoDbContext : AbpMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    public readonly IMongoDatabase _mongoDatabase;
    public CinemaManagementMongoDbContext()
    {
        var client = new MongoClient("mongodb://localhost:27017");
         _mongoDatabase = client.GetDatabase("CinemaManagement");
    }
    public IMongoCollection<screenType> ScreenTypes => _mongoDatabase.GetCollection<screenType>("ScreenTypes");
    public IMongoCollection<countrie> Countries => _mongoDatabase.GetCollection<countrie>("Countries");
    public IMongoCollection<genre> Genres => _mongoDatabase.GetCollection<genre>("Genres");
    public IMongoCollection<movie> Movies  => _mongoDatabase.GetCollection<movie>("Movies");
    public IMongoCollection<cinemaZoom> CinemaZooms  => _mongoDatabase.GetCollection<cinemaZoom>("CinemaZooms");
    public IMongoCollection<showTime> ShowTimes => _mongoDatabase.GetCollection<showTime>("ShowTimes");
    public IMongoCollection<ticket> Tickets => _mongoDatabase.GetCollection<ticket>("Tickets");

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        //modelBuilder.Entity<YourEntity>(b =>
        //{
        //    //...
        //});
    }
}
