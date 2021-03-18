using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Net.Business.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Net.Data
{
    public class DriveApiService
    {
        protected static string[] scopes = { DriveService.Scope.Drive };
        protected readonly UserCredential credential;
        static string ApplicationName = "AuditoriaExtranet";
        static string urlMaster = "https://drive.google.com/open?id=";
        protected readonly DriveService service;
        protected readonly FileExtensionContentTypeProvider fileExtensionProvider;

        //https://dev.to/theonlybeardedbeast/using-google-drive-in-a-c-application-38ag
        public DriveApiService()
        {

            var path = Path.Combine(Environment.CurrentDirectory, "extranet_file", "client_secret.json");

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "auditoria@invetsa.com", // use a const or read it from a config file
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

                fileExtensionProvider = new FileExtensionContentTypeProvider();
            }

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public IList<Google.Apis.Drive.v3.Data.File> ListEntities(string id = "root")
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 100;
            listRequest.Fields = "nextPageToken, files(id, name, parents, createdTime, modifiedTime, mimeType)";
            listRequest.Q = $"'{id}' in parents";

            return listRequest.Execute().Files;
        }

        public string GenerateDirectory(string Empresa, string Planta, string TipoExplotacion, string SubTipoExplotacion, string Anio, string Mes)
        {
            var Id = CreateFolder(Empresa, "root");

            Id = CreateFolder(Planta, Id);

            Id = CreateFolder(TipoExplotacion, Id);

            Id = CreateFolder(SubTipoExplotacion, Id);

            Id = CreateFolder(Anio, Id);

            Id = CreateFolder(Mes, Id);

            return Id;
        }

        public string CreateFolder(string name, string id = "root")
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                WritersCanShare = false,
                Name = name,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new[] { id }
            };

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Q = "('" + id + "' in parents)";
            //listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name, mimeType)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.MimeType == "application/vnd.google-apps.folder")
                    {

                        if (file.Name == fileMetadata.Name)
                        {
                            return file.Id;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
            Console.WriteLine("Creating new file...");

            var request = service.Files.Create(fileMetadata);
            //request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType";

            var fileRequest = request.Execute();

            return fileRequest.Id;
        }

        public IEnumerable<BE_GoogleDriveFiles> GetFolderChildren(string id = "root")
        {

            List<BE_GoogleDriveFiles> list = new List<BE_GoogleDriveFiles>();

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Q = "('" + id + "' in parents)";
            //listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name, mimeType, parents)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    var itenGoogle = new BE_GoogleDriveFiles
                    {
                        IdGoogleDrive = file.Id,
                        Names = file.Name,
                        MimeType = file.MimeType
                    };

                    list.Add(itenGoogle);
                }
            }

            //GetPremissionDetails(id);

            return list;
        }

        public IList<Google.Apis.Drive.v3.Data.Permission> GetPremissionDetails(string fileId)
        {
            FilesResource.GetRequest getReq = service.Files.Get(fileId);
            getReq.Fields = "permissions";

            Google.Apis.Drive.v3.Data.File file = getReq.Execute();

            return file.Permissions;
        }

        public bool FileSharePermission(string fileId, string permissionValue, string userRule)
        {
            //GetPremissionDetails(fileId);

            bool message = false;
            try
            {

                if (string.IsNullOrEmpty(userRule))
                {
                    userRule = "reader";
                }
                
                Google.Apis.Drive.v3.Data.Permission permission = new Google.Apis.Drive.v3.Data.Permission();
                permission.Type = "user"; // "user, anyone"
                permission.EmailAddress = permissionValue;
                permission.Role = userRule;

                permission =  service.Permissions.Create(permission, fileId).Execute();

                if (permission != null)
                {
                    message = true;
                }

                return message;
            }
            catch (Exception ex)
            {
                message = false;
            }

            return message;
        }

        public async Task<int> MoveFile(string fileId, string FolderId)
        {
            Google.Apis.Drive.v3.FilesResource.GetRequest getRequest = service.Files.Get(fileId);
            getRequest.Fields = "parents";
            Google.Apis.Drive.v3.Data.File file = getRequest.Execute();
            string previousParents = string.Join(",", file.Parents);

            Google.Apis.Drive.v3.FilesResource.UpdateRequest updateRequest = service.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
            updateRequest.Fields = "parents";
            updateRequest.AddParents = FolderId;
            updateRequest.RemoveParents = previousParents;

            file = updateRequest.Execute();

            if (file != null)
            {
                return 1;
            } else
            {
                return 0;
            }
        }

        /// <summary>
        /// Subir archivo
        /// La carga de archivos es similar a la creación de una carpeta
        /// </summary>
        /// <param name="file"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<Google.Apis.Drive.v3.Data.File> Upload(IFormFile file, string documentId, string nameFile)
        {
            var name = ($"{nameFile}{Path.GetExtension(file.FileName)}");
            //var name = nameFile;
            var mimeType = file.ContentType;

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                CopyRequiresWriterPermission = true,
                WritersCanShare = false,
                Name = name,
                MimeType = mimeType,
                Parents = new[] { documentId }
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = file.OpenReadStream())
            {
                request = service.Files.Create(fileMetadata, stream, mimeType);
                request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType, thumbnailLink";
                await request.UploadAsync();
            }

            return request.ResponseBody;
        }

        /// <summary>
        /// Cambiar el nombre de archivos / carpetas
        /// En nuestra aplicación solo podemos editar el nombre del archivo o carpeta, pero siguiendo este ejemplo, 
        /// puede cambiar cualquier cosa en la entidad del archivo.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public void Rename(string name, string id)
        {
            Google.Apis.Drive.v3.Data.File file = service.Files.Get(id).Execute();

            var update = new Google.Apis.Drive.v3.Data.File();
            update.Name = name;

            service.Files.Update(update, id).Execute();
        }

        /// <summary>
        /// Eliminando archivo / carpeta
        /// </summary>
        /// <param name="id"></param>
        public Task Remove(string id)
        {
            return Task.Run(() => service.Files.Delete(id).Execute());
        }

        /// <summary>
        /// Descarga de archivos
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<BE_MemoryStream> Download(string fileId)
        {
            var outputstream = new MemoryStream();
            var request = service.Files.Get(fileId);

            var name = request.Execute().Name;

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
                            //SaveStream(outputstream, FilePath);
                            break;
                        }
                    case DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };

            await request.DownloadAsync(outputstream);

            outputstream.Position = 0;

            return new BE_MemoryStream
            {
                FileMemoryStream = outputstream,
                TypeFile = GetContentType(name),
                NameFile = Path.GetFileName(name)
            };
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".mp3", "audio/mpeg"},
                {".mp4", "video/mp4"}
            };
        }

        // file save to server path
        private static void SaveStream(MemoryStream stream, string FilePath)
        {
            using (System.IO.FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }
    }
}
