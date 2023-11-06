using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CinemaManagement.Data;

/* This is used if database provider does't define
 * ICinemaManagementDbSchemaMigrator implementation.
 */
public class NullCinemaManagementDbSchemaMigrator : ICinemaManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
