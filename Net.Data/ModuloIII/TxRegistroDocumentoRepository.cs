using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Moq;
using Net.Business.Entities;
using Net.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        const string SP_GET_EMPRESA_X_USUARIO = DB_ESQUEMA + "EXT_GetTxRegistroDocumentoEmpresa";
        const string SP_GET_PLANTA_X_USUARIO = DB_ESQUEMA + "SEG_GetPlantaPorIdUsuario";
        const string SP_GET_ID = DB_ESQUEMA + "EXT_GetTxRegistroDocumentoPorId";
        const string SP_GET_ID_DOCUMENTO = DB_ESQUEMA + "EXT_GetTxRegistroDocumentoPorIdDocumento";
        const string SP_INSERT = DB_ESQUEMA + "EXT_SetTxRegistroDocumentoInsert";
        const string SP_DELETE = DB_ESQUEMA + "EXT_SetTxRegistroDocumentoDelete";
        const string SP_UPDATE = DB_ESQUEMA + "EXT_SetTxRegistroDocumentoUpdate";

        const string SP_UPDATESTATUS = DB_ESQUEMA + "EXT_SetTxRegistroDocumentoUpdateStatus";

        // Validacion de existencia de IdDriveGoogle

        const string SP_GET_GOOGLE_DRIVE_ID = DB_ESQUEMA + "EXT_GetTxRegistroDocumentoFolderPorFiltros";
        const string SP_GOOGLE_DRIVE_INSERT = DB_ESQUEMA + "EXT_SetTxRegistroDocumentoFolderInsert";

        const string SP_GOOGLE_DRIVE_EMPRESA_MERGE = DB_ESQUEMA + "EXT_SetTxRegistroDocumentoEmpresaMerge";

        public TxRegistroDocumentoRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_TxRegistroDocumento>> GetAll(BE_TxRegistroDocumento entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_TxRegistroDocumento> GetByIdDocumento(BE_TxRegistroDocumento entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID_DOCUMENTO));
        }

        public Task<IEnumerable<BE_GoogleDriveFiles>> GetAllEmpresaPorUsuario(BE_GoogleDriveFiles entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_GoogleDriveFiles>(SP_GET_EMPRESA_X_USUARIO, entidad));
        }

        public Task<IEnumerable<BE_GoogleDriveFiles>> GetGoogleDriveFilesPorId(BE_GoogleDriveFiles entidad)
        {
            return Task.Run(() =>
            {
                DriveApiService apiService = new DriveApiService();
                var lista = apiService.GetFolderChildren(entidad.IdGoogleDrive);
                return lista;
            });
        }

        public IEnumerable<BE_TxRegistroDocumento> GetAllCalidad(BE_TxRegistroDocumento entidad)
        {
            return FindAll(entidad, SP_GET);
        }
        public Task<BE_TxRegistroDocumento> GetById(BE_TxRegistroDocumento entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<BE_ResultadoTransaccion> Create(BE_TxRegistroDocumento entidad, IList<IFormFile> lista_anexo, bool generaNombre = true)
        {
            BE_ResultadoTransaccion vResultadoTransaccion = new BE_ResultadoTransaccion();
            vResultadoTransaccion.ResultadoCodigo = 1;

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
                            Google.Apis.Drive.v3.Data.File fileGoogleDrive = new Google.Apis.Drive.v3.Data.File();

                            var _anio = DateTime.Now.Year;
                            var _mes = DateTime.Now.Month;

                            DateTimeFormatInfo dateTimeFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
                            string nombreMes = dateTimeFormatInfo.GetMonthName(_mes);

                            entidad.Ano = _anio;
                            entidad.Mes = _mes;
                            try
                            {
                                DriveApiService googleApiDrive = new DriveApiService();

                                // Validamos si existe Id de folder en Google Drive
                                string IdFolderGoogleDrive = string.Empty;

                            
                                var IdFolderEmpresaGoogleDrive = googleApiDrive.CreateFolder(entidad.DescripcionEmpresa);

                                using (SqlCommand cmd = new SqlCommand(SP_GOOGLE_DRIVE_EMPRESA_MERGE, conn))
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd.Parameters.Add(new SqlParameter("@CodigoEmpresa", entidad.CodigoEmpresa));
                                    cmd.Parameters.Add(new SqlParameter("@IdGoogleDrive", IdFolderEmpresaGoogleDrive));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", entidad.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", entidad.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }

                                IdFolderGoogleDrive = GetIdDriveFolder(new BE_TxRegistroDocumentoFolder
                                {
                                    IdSubTipoExplotacion = entidad.IdSubTipoExplotacion,
                                    CodigoEmpresa = entidad.CodigoEmpresa,
                                    CodigoPlanta = entidad.CodigoPlanta,
                                    Ano = entidad.Ano,
                                    Mes = entidad.Mes
                                });

                                if (string.IsNullOrEmpty(IdFolderGoogleDrive))
                                {
                                    IdFolderGoogleDrive = googleApiDrive.GenerateDirectory(entidad.DescripcionEmpresa, entidad.DescripcionPlanta, entidad.DescripcionTipoExplotacion, entidad.DescripcionSubTipoExplotacion, entidad.Ano.ToString(), nombreMes);

                                    using (SqlCommand cmd = new SqlCommand(SP_GOOGLE_DRIVE_INSERT, conn))
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.Add(new SqlParameter("@IdSubTipoExplotacion", entidad.IdSubTipoExplotacion));
                                        cmd.Parameters.Add(new SqlParameter("@CodigoEmpresa", entidad.CodigoEmpresa));
                                        cmd.Parameters.Add(new SqlParameter("@CodigoPlanta", entidad.CodigoPlanta));
                                        cmd.Parameters.Add(new SqlParameter("@IdGoogleDrive", IdFolderGoogleDrive));
                                        cmd.Parameters.Add(new SqlParameter("@RegUsuario", entidad.RegUsuario));
                                        cmd.Parameters.Add(new SqlParameter("@RegEstacion", entidad.RegEstacion));

                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                }

                                if (entidad.FlgCerrado == null)
                                {
                                    var IdFolderPendienteCierreGoogleDrive = googleApiDrive.CreateFolder("PENDIENTE CIERRE");
                                    IdFolderGoogleDrive = IdFolderPendienteCierreGoogleDrive;
                                }

                                using (SqlCommand cmd = new SqlCommand(SP_INSERT, conn))
                                {
                                    foreach (IFormFile _lista in lista_anexo)
                                    {

                                        extension = _lista.FileName.Split('.').Last();
                                        nombre_archivo = string.Empty;
                                        nombre_archivo = _lista.FileName.Replace("." + extension,string.Empty);

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
                                        
                                        cmd.Parameters.Add(new SqlParameter("@GeneraNombre", generaNombre));
                                        cmd.Parameters.Add(new SqlParameter("@NombreOriginal", nombre_archivo));

                                        SqlParameter oParamNombreArchivo = new SqlParameter("@NombreArchivo", SqlDbType.VarChar, 300)
                                        {
                                            Direction = ParameterDirection.Output

                                        };
                                        cmd.Parameters.Add(oParamNombreArchivo);
                                        SqlParameter oParamCadenaEmail = new SqlParameter("@CadenaEmail", SqlDbType.VarChar, 300)
                                        {
                                            Direction = ParameterDirection.Output

                                        };
                                        cmd.Parameters.Add(oParamCadenaEmail);
                                        cmd.Parameters.Add(new SqlParameter("@TipoArchivo", _lista.ContentType));
                                        cmd.Parameters.Add(new SqlParameter("@ExtencionArchivo", extension));
                                        cmd.Parameters.Add(new SqlParameter("@FlgCerrado", entidad.FlgCerrado));
                                        cmd.Parameters.Add(new SqlParameter("@IdUsuarioCierre", entidad.IdUsuarioCierre));
                                        cmd.Parameters.Add(new SqlParameter("@FecCerrado", entidad.FecCerrado));
                                        cmd.Parameters.Add(new SqlParameter("@IdDocumentoReferencial", entidad.IdDocumentoReferencial));
                                        cmd.Parameters.Add(new SqlParameter("@RegUsuario", entidad.RegUsuario));
                                        cmd.Parameters.Add(new SqlParameter("@RegEstacion", entidad.RegEstacion));

                                        await cmd.ExecuteNonQueryAsync();

                                        entidad.IdDocumento = (int)cmd.Parameters["@IdDocumento"].Value;
                                        entidad.NombreArchivo = (string)oParamNombreArchivo.Value;

                                        var cadenaEmail = (string)oParamCadenaEmail.Value;

                                        nombre_archivo = entidad.NombreArchivo + "." + extension.ToString();

                                        fileGoogleDrive = await googleApiDrive.Upload(_lista, IdFolderGoogleDrive, entidad.NombreArchivo);

                                        if (!string.IsNullOrEmpty(fileGoogleDrive.Id))
                                        {
                                            using (SqlCommand cmdIdGoogle = new SqlCommand(SP_UPDATE, conn))
                                            {
                                                cmdIdGoogle.Parameters.Clear();
                                                cmdIdGoogle.CommandType = System.Data.CommandType.StoredProcedure;
                                                cmdIdGoogle.Parameters.Add(new SqlParameter("@IdDocumento", entidad.IdDocumento));
                                                cmdIdGoogle.Parameters.Add(new SqlParameter("@IdGoogleDrive", fileGoogleDrive.Id));
                                                cmdIdGoogle.Parameters.Add(new SqlParameter("@RegUsuario", entidad.RegUsuario));
                                                cmdIdGoogle.Parameters.Add(new SqlParameter("@RegEstacion", entidad.RegEstacion));

                                                await cmdIdGoogle.ExecuteNonQueryAsync();
                                            }
                                        }
                                        if (entidad.FlgCerrado == null)
                                        {
                                            try
                                            {
                                                if (!string.IsNullOrEmpty(cadenaEmail))
                                                {
                                                    EmailSenderRepository emailSenderRepository = new EmailSenderRepository(context);
                                                    var mensaje = string.Format("Buen día, <br>  <br> Se realizo la carga de un nuevo archivo a la extranet N° {0}, Favor su aprobación", entidad.IdDocumento);
                                                    var link = string.Format(" <br>  <br><a href=\"https://auditoria.invetsa.com/Invetsa/\">Ingresar a la Aplicación</a>");
                                                    await emailSenderRepository.SendEmailAsync(cadenaEmail, "Correo Automatico - Registro de Documento - Extranet", mensaje + "  " + link);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                vResultadoTransaccion.ResultadoCodigo = -1;
                                                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                                                return vResultadoTransaccion;
                                            }
                                        }

                                    }
                                }

                                transaction.Commit();
                            }
                            catch (System.Exception ex)
                            {
                                vResultadoTransaccion.ResultadoCodigo = -1;
                                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                                transaction.Rollback();
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
            }

            return vResultadoTransaccion;
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

        public async Task<BE_MemoryStream> GetDownloadFileGoogleDrive(BE_TxRegistroDocumento entidad)
        {
            DriveApiService googleApiDrive = new DriveApiService();
            var data = await googleApiDrive.Download(entidad.IdGoogleDrive);


            return data;
        }

        //public async Task<BE_File> GetDownloadFileGoogleDriveBase64(BE_TxRegistroDocumento entidad)
        //{
        //    DriveApiService googleApiDrive = new DriveApiService();
        //    var data = await googleApiDrive.Download(entidad.IdGoogleDrive);

        //    byte[] bytes;
        //    bytes = data.FileMemoryStream.ToArray();

        //    string base64 = Convert.ToBase64String(bytes);

        //    return new BE_File { 
        //        FileBase64 = base64,
        //        Name = data.NameFile,
        //        Type = data.TypeFile
        //    };
        //}

        public async Task<BE_File> GetDownloadFileGoogleDriveSave(BE_TxRegistroDocumento entidad)
        {
            DriveApiService googleApiDrive = new DriveApiService();
            var data = await googleApiDrive.Download(entidad.IdGoogleDrive);

            byte[] bytes;
            bytes = data.FileMemoryStream.ToArray();

            var nombreAleatorio = GenerarCodigo();

            /*
                Desarrollo
                    \\\\SERVIDOR95\\Users\\InvetsaNet\\Documents\\Auditoria\\INCUBA-FrontEnd\\src\\assets\\file-pdf\\
                Produccion
                    \\\\SERVIDOR96\\Users\\adminauditoria\\Documents\\Auditoria\\Invetsa\\assets\\file-pdf\\
             */

            using (FileStream
            fileStream = new FileStream("\\\\SERVIDOR96\\Users\\adminauditoria\\Documents\\Auditoria\\Invetsa\\assets\\file-pdf\\" + nombreAleatorio + ".pdf", FileMode.Create))
            {
                // Write the data to the file, byte by byte.
                for (int i = 0; i < bytes.Length; i++)
                {
                    fileStream.WriteByte(bytes[i]);
                }

                // Set the stream position to the beginning of the file.
                fileStream.Seek(0, SeekOrigin.Begin);
 
            }

            return new BE_File
            {
                Name = data.NameFile,
                NameAleatorio = nombreAleatorio,
                Type = data.TypeFile
            };
        }

        private string GenerarCodigo()
        {
            Random obj = new Random();
            string sCadena = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = sCadena.Length;
            char cletra;
            int nlongitud = 30;
            string sNuevacadena = string.Empty;

            for (int i = 0; i < nlongitud; i++)
            {
                cletra = sCadena[obj.Next(nlongitud)];
                sNuevacadena += cletra.ToString();
            }
            return sNuevacadena;

        }

        public async Task<BE_MemoryStream> GetDownloadFileServidorLocal(BE_TxRegistroDocumento entidad)
        {
            try
            {

                entidad.IdDocumento = 26;
                entidad.IdGoogleDrive = null;
                var data = FindById(entidad, SP_GET_ID);

                var ruta = "\\\\SERVIDOR95\\Users\\InvetsaNet\\Documents\\Auditoria\\extranet_file\\";

                var path = ruta + data.IdGoogleDrive + data.NombreArchivo + '.' + data.ExtencionArchivo;

                var memory = new MemoryStream();

                if (!System.IO.Directory.Exists(ruta + data.IdGoogleDrive))
                {
                    System.IO.Directory.CreateDirectory(ruta + data.IdGoogleDrive);
                }

                //DirectoryInfo di = new DirectoryInfo(ruta + data.IdGoogleDrive);
                //Console.WriteLine("No search pattern returns:");
                //var rutaFile = string.Empty;
                //foreach (var fi in di.GetFiles())
                //{
                //    if (fi.Name == data.NombreArchivo)
                //    {
                //        rutaFile = fi.FullName;
                //    }

                //}

                if (!File.Exists(path))
                {
                    return null;
                }

                var file = File.ReadAllBytes(path);

                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return new BE_MemoryStream
                {
                    FileMemoryStream = memory,
                    //TypeFile = GetContentType(path),
                    NameFile = Path.GetFileName(path)
                };
            }
            catch (Exception ex)
            {
                return new BE_MemoryStream();
            }
        }
        public Task<BE_ResultadoTransaccion> Update(BE_TxRegistroDocumento entidad)
        {
            return Task.Run(() => {

                BE_ResultadoTransaccion vResultadoTransaccion = new BE_ResultadoTransaccion();
                vResultadoTransaccion.ResultadoCodigo = 1;

                try
                {
                    DriveApiService driveApiService = new DriveApiService();
                    var resp = driveApiService.MoveFile(entidad.IdGoogleDrive, entidad.IdGoogleDriveFolder);

                    if (resp.Result == 0)
                    {
                        vResultadoTransaccion.ResultadoCodigo = -1;
                        vResultadoTransaccion.ResultadoDescripcion = "Ocurrio un error al mover archivo en el google drive";
                        return vResultadoTransaccion;
                    }
                }
                catch (Exception ex)
                {
                    vResultadoTransaccion.ResultadoCodigo = -1;
                    vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                    return vResultadoTransaccion;
                }

                try
                {
                    entidad.IdGoogleDrive = null;
                    entidad.IdGoogleDriveFolder = null;
                    Update(entidad, SP_UPDATESTATUS);
                }
                catch (Exception ex)
                {
                    vResultadoTransaccion.ResultadoCodigo = -1;
                    vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                    return vResultadoTransaccion;
                }

                return vResultadoTransaccion;
            });
        }
        public Task Delete(BE_TxRegistroDocumento entidad)
        {
            return Task.Run(() => {
                DriveApiService googleApiDrive = new DriveApiService();
                googleApiDrive.Remove(entidad.IdGoogleDrive);
                Delete(entidad, SP_DELETE);
            });
        }

        public Task<bool> GetUrlFileGoogleDrive(string id, string permissionValue, string userRule)
        {
            return Task.Run(() => {
                DriveApiService googleApiDrive = new DriveApiService();
                var data =  googleApiDrive.FileSharePermission(id, permissionValue, userRule);
                return data;
            });
        }
    }
}