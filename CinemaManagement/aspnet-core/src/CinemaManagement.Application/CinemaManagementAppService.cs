using System;
using System.Collections.Generic;
using System.Text;
using CinemaManagement.Localization;
using CinemaManagement.Movies;
using MongoDB.Bson;
using MongoDB.Driver;
using Polly;
using Volo.Abp.Application.Services;

namespace CinemaManagement;

/* Inherit your application services from this class.
 */
public abstract class CinemaManagementAppService : ApplicationService
{
    protected CinemaManagementAppService()
    {
        LocalizationResource = typeof(CinemaManagementResource);
    }

    public long FindByCode<T>(IMongoCollection<T> collection, string field, string code)
    {
        var filter = Builders<T>.Filter.Eq(field, code);

        var aggregateResult = collection.Aggregate().Match(filter).FirstOrDefault();

        if (aggregateResult != null)
        {
            return 1;
        }

        return 0;
    }

   protected DateTime timeConvert(DateTime date)
    {

        TimeZoneInfo ictTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");


       return TimeZoneInfo.ConvertTime(date, ictTimeZone);
    }

   
}
