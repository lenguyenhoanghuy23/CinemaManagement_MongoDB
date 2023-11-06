using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace CinemaManagement.DataManagement
{
    public interface IDataManagementAppservice:IApplicationService
    {
        string ExportToJson(List<string> CollectionNames);
        string ImportToJson(List<string> CollectionNames);
        string Backup();
        string reStore(string backupDirectory, string dbName);
        Task<List<string>> ShowCollection();
    }
}
