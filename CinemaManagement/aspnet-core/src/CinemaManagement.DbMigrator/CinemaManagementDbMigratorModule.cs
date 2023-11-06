using CinemaManagement.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace CinemaManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CinemaManagementMongoDbModule),
    typeof(CinemaManagementApplicationContractsModule)
    )]
public class CinemaManagementDbMigratorModule : AbpModule
{
}
