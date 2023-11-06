using CinemaManagement.MongoDB;
using Volo.Abp.Modularity;

namespace CinemaManagement;

[DependsOn(
    typeof(CinemaManagementMongoDbTestModule)
    )]
public class CinemaManagementDomainTestModule : AbpModule
{

}
