using System.Threading.Tasks;

namespace CinemaManagement.Data;

public interface ICinemaManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
