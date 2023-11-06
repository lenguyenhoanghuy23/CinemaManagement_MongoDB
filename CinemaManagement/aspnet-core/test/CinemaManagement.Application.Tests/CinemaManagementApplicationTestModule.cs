using Volo.Abp.Modularity;

namespace CinemaManagement;

[DependsOn(
    typeof(CinemaManagementApplicationModule),
    typeof(CinemaManagementDomainTestModule)
    )]
public class CinemaManagementApplicationTestModule : AbpModule
{

}
