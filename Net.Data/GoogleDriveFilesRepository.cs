using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Net.Business.Entities;
using Net.Connection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Net.Data
{
    public class GoogleDriveFilesRepository: RepositoryBase<BE_GoogleDriveFiles>, IGoogleDriveFilesRepository
    {
        public GoogleDriveFilesRepository(IConnectionSQL context)
           : base(context)
        {
        }
        //defined scope.
        public static string[] Scopes = { DriveService.Scope.Drive };

        //create Drive API service.
        public static DriveService GetService()
        {
            //get Credentials from client_secret.json file 
            UserCredential credential;
            using (var stream = new FileStream(@"C:\Users\InvetsaNet\Documents\Auditoria\extranet_file\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                String FolderPath = @"C:\Users\InvetsaNet\Documents\Auditoria\extranet_file\";
                String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(FilePath, true)).Result;
            }

            //create Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MyProjectExtranet",
            });
            return service;
        }

        //get all files from Google Drive.
        public Task<List<BE_GoogleDriveFiles>> GetDriveFiles()
        {
            return Task.Run(() => {
                DriveService service = GetService();

                // define parameters of request.
                FilesResource.ListRequest FileListRequest = service.Files.List();

                //listRequest.PageSize = 10;
                //listRequest.PageToken = 10;
                FileListRequest.Fields = "nextPageToken, files(id, name, size, version, createdTime)";

                //get file list.
                IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
                List<BE_GoogleDriveFiles> FileList = new List<BE_GoogleDriveFiles>();

                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        BE_GoogleDriveFiles File = new BE_GoogleDriveFiles
                        {
                            Id = file.Id,
                            Name = file.Name,
                            Size = file.Size,
                            Version = file.Version,
                            CreatedTime = file.CreatedTime
                        };
                        FileList.Add(File);
                    }
                }

                return FileList;
            });
        }

        //file Upload to the Google Drive.
        //Task FileUpload(IFormFile file, string path)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        DriveService service = GetService();

        //        //string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
        //        //Path.GetFileName(file.FileName));
        //        //file.(path);

        //        var FileMetaData = new Google.Apis.Drive.v3.Data.File();
        //        FileMetaData.Name = Path.GetFileName(file.FileName);
        //        //FileMetaData.MimeType = MimeMapping.GetMimeMapping(path);

        //        FilesResource.CreateMediaUpload request;

        //        using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
        //        {
        //            request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
        //            request.Fields = "id";
        //            request.Upload();
        //        }
        //    }
        //}

        //Download file from Google Drive by fileId.
        public static string DownloadGoogleFile(string fileId, string FolderPath)
        {
            DriveService service = GetService();

            //string FolderPath = HttpContext. .Server.MapPath("/GoogleDriveFiles/");
            FilesResource.GetRequest request = service.Files.Get(fileId);

            string FileName = request.Execute().Name;
            string FilePath = System.IO.Path.Combine(FolderPath, FileName);

            MemoryStream stream1 = new MemoryStream();

            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            SaveStream(stream1, FilePath);
                            break;
                        }
                    case DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.Download(stream1);
            return FilePath;
        }

        // file save to server path
        private static void SaveStream(MemoryStream stream, string FilePath)
        {
            using (System.IO.FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        //Delete file from the Google drive
        public static void DeleteFile(BE_GoogleDriveFiles files)
        {
            DriveService service = GetService();
            try
            {
                // Initial validation.
                if (service == null)
                    throw new ArgumentNullException("service");

                if (files == null)
                    throw new ArgumentNullException(files.Id);

                // Make the request.
                service.Files.Delete(files.Id).Execute();
            }
            catch (Exception ex)
            {
                throw new Exception("Request Files.Delete failed.", ex);
            }
        }
    }
}
