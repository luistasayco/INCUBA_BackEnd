using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
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
        static string ApplicationName = "MyProjectExtranet";
        protected readonly DriveService service;
        protected readonly FileExtensionContentTypeProvider fileExtensionProvider;

        //https://dev.to/theonlybeardedbeast/using-google-drive-in-a-c-application-38ag
        public DriveApiService()
        {
           
            using (var stream = new FileStream(@"C:\Users\InvetsaNet\Documents\Auditoria\extranet_file\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "ing.luis.tasayco@gmail.com", // use a const or read it from a config file
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

        public string GenerateDirectory(string TipoExplotacion, string SubTipoExplotacion, string Empresa, string Planta, string Anio, string Mes)
        {
            var Id = CreateFolder(TipoExplotacion, "root");

            Id = CreateFolder(SubTipoExplotacion, Id);

            Id = CreateFolder(Empresa, Id);

            Id = CreateFolder(Planta, Id);

            Id = CreateFolder(Anio, Id);

            Id = CreateFolder(Mes, Id);

            return Id;
        }

        public string CreateFolder(string name, string id = "root")
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new[] { id }
            };

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Name == fileMetadata.Name)
                    {
                        return file.Id;
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

        /// <summary>
        /// Subir archivo
        /// La carga de archivos es similar a la creación de una carpeta
        /// </summary>
        /// <param name="file"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<Google.Apis.Drive.v3.Data.File> Upload(IFormFile file, string documentId, string nameFile)
        {
            //var name = ($"{nameFile}.{Path.GetExtension(file.FileName)}");
            var name = file.FileName;
            var mimeType = file.ContentType;

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
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
        public void Remove(string id)
        {
            service.Files.Delete(id).Execute();
        }

        /// <summary>
        /// Descarga de archivos
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<MemoryStream> Download(string fileId)
        {
            var outputstream = new MemoryStream();
            var request = service.Files.Get(fileId);

            await request.DownloadAsync(outputstream);

            outputstream.Position = 0;

            return outputstream;
        }
    }
}
