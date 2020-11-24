using Microsoft.AspNetCore.Http;
using Net.Business.Entities;
using Net.Connection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IGoogleDriveFilesRepository : IRepositoryBase<BE_GoogleDriveFiles>
    {
        Task<List<BE_GoogleDriveFiles>> GetDriveFiles();
        //Task FileUpload(IFormFile file, string path);
    }
}
