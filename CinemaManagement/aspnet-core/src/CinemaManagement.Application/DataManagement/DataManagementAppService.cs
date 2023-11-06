using CinemaManagement.MongoDB;
using CinemaManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Application.Services;

namespace CinemaManagement.DataManagement
{
    [Authorize(CinemaManagementPermissions.DataManagement.Default)]
    public class DataManagementAppService : ApplicationService, IDataManagementAppservice
    {
        private readonly CinemaManagementMongoDbContext _context;
        public DataManagementAppService(CinemaManagementMongoDbContext context) 
        {
            _context = context;
        }

        [Authorize(CinemaManagementPermissions.DataManagement.Create)]
        public string Backup()
        {
            try
            {
                string exportPath = "D:\\MongoDbExp\\nhom15\\Backup";
                string databaseName = "CinemaManagement";
                string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "mongodump",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Arguments = $"--db {databaseName} --out \"{Path.Combine(exportPath, $"{databaseName}_{currentDateTime}")}\""
                    }
                };

                process.Start();
                process.WaitForExit();

                // Trả về một thông báo hoặc giá trị tùy theo yêu cầu của bạn
                return $"Backup completed at {currentDateTime}";
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [Authorize(CinemaManagementPermissions.DataManagement.Create)]
        public string ExportToJson(List<string> CollectionNames)
        {
            try
            {
                if (CollectionNames == null || CollectionNames.Count == 0)
                {
                    return "Vui lòng cung cấp danh sách các collection cần export";
                }
                string exportPath = "D:\\MongoDbExp\\nhom15\\export";
                string databaseName = "CinemaManagement";

                foreach (var collectionName in CollectionNames)
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            //FileName = @"C:\Program Files\MongoDB\Server\4.2\bin\mongoexport",
                            FileName = "mongoexport",
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            Arguments = $"--db CinemaManagement --collection {collectionName} --out {Path.Combine(exportPath, $"{databaseName}.{collectionName}.json")}"
                        }
                    };
                    process.Start();
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        return $"Lỗi trong quá trình xuất dữ liệu từ collection {collectionName} ";
                    }
                }

                // Nếu không có lỗi, xuất thông báo thành công
                return "Thành công";
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }
        [Authorize(CinemaManagementPermissions.DataManagement.Create)]
        public string ImportToJson(List<string> CollectionNames)
        {

            throw new NotImplementedException();
        }
        [Authorize(CinemaManagementPermissions.DataManagement.Create)]
        public string reStore(string backupDirectory, string dbName)
        {
            try
            {
                if (backupDirectory == null || backupDirectory.Length == 0)
                {
                    return "Tệp backup không hợp lệ.";
                }
                // Đường dẫn tới thư mục lưu tệp backup tạm thời
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "mongorestore",
                        //RedirectStandardOutput = true,
                        //UseShellExecute = false,
                        //CreateNoWindow = true,
                        Arguments = $"--db {dbName} --dir \"{backupDirectory}\""
                    }
                };
                process.Start();
                process.WaitForExit();

                // Xử lý kết quả hoặc thông báo thành công/lỗi
                if (Directory.GetFiles(backupDirectory).Any())
                {
                    // Có tệp được tạo ra, thông báo thành công
                    return "Khôi phục dữ liệu thành công.";
                }
                else
                {
                    // Không có tệp được tạo ra, thông báo lỗi
                    return "Khôi phục dữ liệu thất bại. Không có tệp nào được tạo ra.";
                }
            }
            catch (Exception ex)
            {
                return $"Có lỗi xảy ra trong quá trình khôi phục dữ liệu. {ex}";
            }
        }

        public async Task<List<string>> ShowCollection()
        {
            var collectionNames = await _context._mongoDatabase.ListCollectionNames().ToListAsync(); // Sử dụng dự án trung gian hoặc service để lấy danh sách collection

            return collectionNames;

        }
    }
}
