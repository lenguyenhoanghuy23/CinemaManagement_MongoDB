using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace CinemaManagement.MongoDB;

[DependsOn(
    typeof(CinemaManagementTestBaseModule),
    typeof(CinemaManagementMongoDbModule)
    )]
public class CinemaManagementMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = CinemaManagementMongoDbFixture.GetRandomConnectionString();
        });
    }
}
