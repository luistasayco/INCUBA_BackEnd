using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Net.Business.Entities;
using Net.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Net.Data
{
    public class TxRegistroDocumentoRepository : RepositoryBase<BE_TxRegistroDocumento>, ITxRegistroDocumentoRepository
    {
        //private readonly string _ubicacion;

        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "EXT_GetTxRegistroDocumentoPorFiltro";
        const string SP_GET_ID = DB_ESQUEMA + "EXT_GetTxRegistroDocumentoPorId";
        const string SP_INSERT = DB_ESQUEMA + "EXT_SetTxRegistroDocumentoInsert";
        const string SP_DELETE = DB_ESQUEMA + "EXT_SetTxRegistroDocumentoDelete";
        const string SP_UPDATE = DB_ESQUEMA + "EXT_SetTxRegistroDocumentoUpdate";

        // Validacion de existencia de IdDriveGoogle

        const string SP_GET_GOOGLE_DRIVE_ID = DB_ESQUEMA + "EXT_GetTxRegistroDocumentoFolderPorFiltros";
        const string SP_GOOGLE_DRIVE_INSERT = DB_ESQUEMA + "EXT_SetTxRegistroDocumentoFolderInsert";

        public TxRegistroDocumentoRepository(IConnectionSQL context)
            : base(context)
        {
            //_ubicacion = "\\\\SERVIDOR95\\Users\\InvetsaNet\\Documents\\Auditoria\\extranet_file\\";
        }
        public Task<IEnumerable<BE_TxRegistroDocumento>> GetAll(BE_TxRegistroDocumento entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }

        public IEnumerable<BE_TxRegistroDocumento> GetAllCalidad(BE_TxRegistroDocumento entidad)
        {
            return FindAll(entidad, SP_GET);
        }
        public Task<BE_TxRegistroDocumento> GetById(BE_TxRegistroDocumento entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_TxRegistroDocumento entidad, IList<IFormFile> lista_anexo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(context.DevuelveConnectionSQL()))
                {
                    using (CommittableTransaction transaction = new CommittableTransaction())
                    {
                        await conn.OpenAsync();
                        conn.EnlistTransaction(transaction);

                        if (lista_anexo.Count > 0)
                        {
                            string extension = string.Empty;
                            string nombre_archivo = string.Empty;

                            var _anio = DateTime.Now.Year;
                            var _mes = DateTime.Now.Month;

                            entidad.Ano = _anio;
                            entidad.Mes = _mes;

                            DriveApiService googleApiDrive = new DriveApiService();

                            // Validamos si existe Id de folder en Google Drive
                            string IdFolderGoogleDrive = GetIdDriveFolder(new BE_TxRegistroDocumentoFolder { IdSubTipoExplotacion = entidad.IdSubTipoExplotacion, CodigoEmpresa = entidad.CodigoEmpresa, CodigoPlanta = entidad.CodigoPlanta, Ano = entidad.Ano, Mes = entidad.Mes });

                            if (string.IsNullOrEmpty(IdFolderGoogleDrive))
                            {
                                IdFolderGoogleDrive = googleApiDrive.GenerateDirectory(entidad.DescripcionTipoExplotacion ,entidad.DescripcionSubTipoExplotacion, entidad.DescripcionEmpresa, entidad.DescripcionPlanta, entidad.Ano.ToString(), entidad.Mes.ToString());
                                var idNew = (int)context.ExecuteSqlInsert<BE_TxRegistroDocumentoFolder>(SP_GOOGLE_DRIVE_INSERT, new BE_TxRegistroDocumentoFolder {IdFolder = 0, IdSubTipoExplotacion = entidad.IdSubTipoExplotacion, CodigoEmpresa = entidad.CodigoEmpresa, CodigoPlanta = entidad.CodigoPlanta, IdGoogleDrive = IdFolderGoogleDrive,  RegUsuario = entidad.RegUsuario, RegEstacion = entidad.RegEstacion});
                            }

                            try
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_INSERT, conn))
                                {
                                    foreach (IFormFile _lista in lista_anexo)
                                    {

                                        extension = _lista.FileName.Split('.').Last();

                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        SqlParameter oParam = new SqlParameter("@IdDocumento", entidad.IdDocumento);
                                        oParam.SqlDbType = SqlDbType.Int;
                                        oParam.Direction = ParameterDirection.Output;
                                        cmd.Parameters.Add(oParam);

                                        cmd.Parameters.Add(new SqlParameter("@IdSubTipoExplotacion", entidad.IdSubTipoExplotacion));
                                        cmd.Parameters.Add(new SqlParameter("@CodigoEmpresa", entidad.CodigoEmpresa));
                                        cmd.Parameters.Add(new SqlParameter("@DescripcionEmpresa", entidad.DescripcionEmpresa));
                                        cmd.Parameters.Add(new SqlParameter("@CodigoPlanta", entidad.CodigoPlanta));

                                        SqlParameter oParamNombreArchivo = new SqlParameter("@NombreArchivo", SqlDbType.VarChar, 300)
                                        {
                                            Direction = ParameterDirection.Output
                                        };
                                        cmd.Parameters.Add(oParamNombreArchivo);

                                        //cmd.Parameters.Add(new SqlParameter("@RutaArchivo", armarRuta));
                                        cmd.Parameters.Add(new SqlParameter("@TipoArchivo", _lista.ContentType));
                                        cmd.Parameters.Add(new SqlParameter("@ExtencionArchivo", extension));
                                        cmd.Parameters.Add(new SqlParameter("@RegUsuario", entidad.RegUsuario));
                                        cmd.Parameters.Add(new SqlParameter("@RegEstacion", entidad.RegEstacion));

                                        await cmd.ExecuteNonQueryAsync();

                                        //var rutaUbicacionSave = _ubicacion + armarRuta;

                                        

                                        entidad.IdDocumento = (int)cmd.Parameters["@IdDocumento"].Value;
                                        entidad.NombreArchivo = (string)oParamNombreArchivo.Value;

                                        nombre_archivo = entidad.NombreArchivo + "." + extension.ToString();

                                        //if (System.IO.File.Exists(rutaUbicacionSave.ToString() + nombre_archivo.ToString()))
                                        //{
                                        //    System.IO.File.Delete(rutaUbicacionSave.ToString() + nombre_archivo.ToString());
                                        //}

                                        //if (!System.IO.Directory.Exists(rutaUbicacionSave))
                                        //{
                                        //    System.IO.Directory.CreateDirectory(rutaUbicacionSave);
                                        //}

                                        //using (FileStream fileStream = System.IO.File.Create(rutaUbicacionSave.ToString() + nombre_archivo.ToString()))
                                        //{
                                        //    _lista.CopyTo(fileStream);
                                        //    fileStream.Flush();
                                        //}

                                        //DriveApiService googleApiDrive = new DriveApiService();
                                        //var dataId = googleApiDrive.CreateFolder(entidad.DescripcionEmpresa, "root");
                                        //_lista.Name = nombre_archivo;
                                        var datGoogle = await googleApiDrive.Upload(_lista, IdFolderGoogleDrive, entidad.NombreArchivo);

                                    }
                                }

                                transaction.Commit();
                            }
                            catch (System.Exception ex)
                            {
                                entidad.IdDocumento = 0;
                                transaction.Rollback();
                            }

                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                entidad.IdDocumento = 0;
            }

            return int.Parse(entidad.IdDocumento.ToString());
        }

        private string GetIdDriveFolder(BE_TxRegistroDocumentoFolder entidad)
        {
            var data = context.ExecuteSqlViewId<BE_TxRegistroDocumentoFolder>(SP_GET_GOOGLE_DRIVE_ID, entidad);

            if (data == null)
            {
                return string.Empty;
            }

            return data.IdGoogleDrive;
        }

        public async Task<BE_MemoryStream> GetDownloadFile(BE_TxRegistroDocumento entidad)
        {
            try
            {

                DriveApiService googleApiDrive = new DriveApiService();
                var data = await googleApiDrive.Download(entidad.IdGoogleDrive);

                //var data = FindById(entidad, SP_GET_ID);

                //var ruta = "//SERVIDOR95/Users/InvetsaNet/Documents/Auditoria/extranet_file";


                //var armarRuta = string.Format("/{0}/{1}/{2}/{3}/{4}/{5}/", data.CodigoEmpresa, data.CodigoPlanta, data.IdTipoExplotacion.ToString(), data.IdSubTipoExplotacion.ToString(), data.Ano.ToString(), data.Mes.ToString());

                //nombre_archivo = entidad.NombreArchivo + "." + extension.ToString();

                //var path = _ubicacion + data.RutaArchivo + data.NombreArchivo + '.'+  data.ExtencionArchivo;

                //var memory = new MemoryStream();

                //if (!System.IO.Directory.Exists(_ubicacion + data.RutaArchivo))
                //{
                //    System.IO.Directory.CreateDirectory(_ubicacion + data.RutaArchivo);
                //}

                //DirectoryInfo di = new DirectoryInfo(_ubicacion + data.RutaArchivo);
                //Console.WriteLine("No search pattern returns:");
                //var rutaFile = string.Empty;
                //foreach (var fi in di.GetFiles())
                //{
                //    if (fi.Name == data.NombreArchivo)
                //    {
                //        rutaFile = fi.FullName;
                //    }

                //}

                //if (!File.Exists(path))
                //{
                //    return null;
                //}

                //var file = File.ReadAllBytes(path);
                //var path = Path.GetFullPath(data.NombreArchivo, ruta + armarRuta);
                //var file = Path.GetFullPath(data.NombreArchivo, _ubicacion + data.RutaArchivo);

                //using (FileStream stream = new FileStream(path, FileMode.Open))
                //{
                //    await stream.CopyToAsync(memory);
                //}
                //memory.Position = 0;

                return new BE_MemoryStream
                {
                    //FileMemoryStream = memory,
                    //TypeFile = GetContentType(path),
                    //NameFile = Path.GetFileName(path)
                };
            }
            catch (Exception ex)
            {
                return null;
            }
            

            //var file = File(memory.GetBuffer(), GetContentType(path), Path.GetFileName(path));

            //return file;
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
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public Task Update(BE_TxRegistroDocumento entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_TxRegistroDocumento entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}