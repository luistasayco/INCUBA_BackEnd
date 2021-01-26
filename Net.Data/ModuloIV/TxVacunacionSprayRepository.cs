using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Moq;
using Net.Business.Entities;
using Net.Connection;
using Net.CrossCotting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Net.Data
{
    public class TxVacunacionSprayRepository : RepositoryBase<BE_TxVacunacionSpray>, ITxVacunacionSprayRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetTxVacunacionSprayPorFiltros";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetTxVacunacionSprayPorId";
        const string SP_GET_ID_NEW = DB_ESQUEMA + "INC_GetTxVacunacionSprayPorIdNew";
        const string SP_GET_ID_GOOGLE_DRIVE = DB_ESQUEMA + "INC_GetTxVacunacionSprayPorIdGoogleDrive";
        const string SP_GET_DETALLE_NEW = DB_ESQUEMA + "INC_GetTxVacunacionSprayDetallePorIdNew";
        const string SP_GET_ID_DETALLE = DB_ESQUEMA + "INC_GetTxVacunacionSprayDetallePorId";
        const string SP_GET_ID_DETALLE_RESULTADO = DB_ESQUEMA + "INC_GetTxVacunacionSprayResultadoPorId";
        const string SP_GET_ID_DETALLE_RESULTADO_NEW = DB_ESQUEMA + "INC_GetTxVacunacionSprayResultadoPorIdNew";
        const string SP_GET_ID_DETALLE_FOTOS = DB_ESQUEMA + "INC_GetTxVacunacionSprayFotosPorId";
        const string SP_GET_ID_DETALLE_MAQUINA = DB_ESQUEMA + "INC_GetTxVacunacionSprayMaquinaPorId";
        const string SP_GET_ID_DETALLE_VACUNA = DB_ESQUEMA + "INC_GetTxVacunacionSprayVacunaPorId";

        const string SP_INSERT = DB_ESQUEMA + "INC_SetTxVacunacionSprayInsert";
        const string SP_MERGE_DETALLE = DB_ESQUEMA + "INC_SetTxVacunacionSprayDetalleMerge";
        const string SP_MERGE_DETALLE_RESULTADO = DB_ESQUEMA + "INC_SetTxVacunacionSprayResultadoMerge";
        const string SP_MERGE_DETALLE_FOTO = DB_ESQUEMA + "INC_SetTxVacunacionSprayFotosMerge";
        const string SP_MERGE_DETALLE_MAQUINA = DB_ESQUEMA + "INC_SetTxVacunacionSprayMaquinaMerge";
        const string SP_MERGE_DETALLE_VACUNA = DB_ESQUEMA + "INC_SetTxVacunacionSprayVacunaMerge";

        const string SP_UPDATE = DB_ESQUEMA + "INC_SetTxVacunacionSprayUpdate";
        const string SP_UPDATE_STATUS = DB_ESQUEMA + "INC_SetTxVacunacionSprayStatusUpdate";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetTxVacunacionSprayDelete";

        public TxVacunacionSprayRepository(IConnectionSQL context)
            : base(context)
        {
        }

        public Task<IEnumerable<BE_TxVacunacionSpray>> GetAll(FE_TxVacunacionSpray entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSpray>(SP_GET, entidad));
        }

        public Task<BE_TxVacunacionSpray> GetById(BE_TxVacunacionSpray entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxVacunacionSpray p = context.ExecuteSqlViewId<BE_TxVacunacionSpray>(SP_GET_ID, entidad);
                if (p != null)
                {
                    p.ListarTxVacunacionSprayDetalle = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSprayDetalle>(SP_GET_ID_DETALLE, entidad).ToList();
                    p.ListarTxVacunacionSprayFotos = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSprayFotos>(SP_GET_ID_DETALLE_FOTOS, entidad).ToList();
                    p.ListarTxVacunacionSprayMaquina = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSprayMaquina>(SP_GET_ID_DETALLE_MAQUINA, entidad).ToList();
                    p.ListarTxVacunacionSprayVacuna = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSprayVacuna>(SP_GET_ID_DETALLE_VACUNA, entidad).ToList();
                    p.ListarTxVacunacionSprayResultado = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSprayResultado>(SP_GET_ID_DETALLE_RESULTADO, entidad).ToList();
                }
                else
                {
                    p = new BE_TxVacunacionSpray();
                }
                return p;
            });
            return objListPrincipal;
        }

        public Task<BE_TxVacunacionSpray> GetByIdNew(BE_TxVacunacionSpray entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxVacunacionSpray p = context.ExecuteSqlViewId<BE_TxVacunacionSpray>(SP_GET_ID_NEW, entidad);
                p.ListarTxVacunacionSprayDetalle = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSprayDetalle>(SP_GET_DETALLE_NEW, entidad).ToList();
                p.ListarTxVacunacionSprayResultado = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSprayResultado>(SP_GET_ID_DETALLE_RESULTADO_NEW, entidad).ToList();
                return p;
            });
            return objListPrincipal;
        }

        public async Task<int> Create(BE_TxVacunacionSpray value)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(context.DevuelveConnectionSQL()))
                {
                    using (CommittableTransaction transaction = new CommittableTransaction())
                    {
                        await conn.OpenAsync();
                        conn.EnlistTransaction(transaction);

                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(SP_INSERT, conn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                SqlParameter oParam = new SqlParameter("@IdVacunacionSpray", value.IdVacunacionSpray);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@CodigoEmpresa", value.CodigoEmpresa));
                                cmd.Parameters.Add(new SqlParameter("@CodigoPlanta", value.CodigoPlanta));
                                cmd.Parameters.Add(new SqlParameter("@Unidad", value.Unidad));
                                cmd.Parameters.Add(new SqlParameter("@FecRegistro", value.FecRegistro));
                                cmd.Parameters.Add(new SqlParameter("@ResponsableInvetsa", value.ResponsableInvetsa));
                                cmd.Parameters.Add(new SqlParameter("@ResponsableIncubadora", value.ResponsableIncubadora));
                                cmd.Parameters.Add(new SqlParameter("@FlgHyLine", value.FlgHyLine));
                                cmd.Parameters.Add(new SqlParameter("@FlgLohman", value.FlgLohman));
                                cmd.Parameters.Add(new SqlParameter("@FlgRoss", value.FlgRoss));
                                cmd.Parameters.Add(new SqlParameter("@FlgCobb", value.FlgCobb));
                                cmd.Parameters.Add(new SqlParameter("@FlgOtros", value.FlgOtros));
                                cmd.Parameters.Add(new SqlParameter("@ObservacionOtros", value.ObservacionOtros));
                                cmd.Parameters.Add(new SqlParameter("@FlgLunes", value.FlgLunes));
                                cmd.Parameters.Add(new SqlParameter("@FlgMartes", value.FlgMartes));
                                cmd.Parameters.Add(new SqlParameter("@FlgMiercoles", value.FlgMiercoles));
                                cmd.Parameters.Add(new SqlParameter("@FlgJueves", value.FlgJueves));
                                cmd.Parameters.Add(new SqlParameter("@FlgViernes", value.FlgViernes));
                                cmd.Parameters.Add(new SqlParameter("@FlgSabado", value.FlgSabado));
                                cmd.Parameters.Add(new SqlParameter("@FlgDomingo", value.FlgDomingo));
                                cmd.Parameters.Add(new SqlParameter("@ObservacionInvetsa", value.ObservacionInvetsa));
                                cmd.Parameters.Add(new SqlParameter("@ObservacionPlanta", value.ObservacionPlanta));
                                cmd.Parameters.Add(new SqlParameter("@PromedioPollos", value.PromedioPollos));

                                cmd.Parameters.Add(new SqlParameter("@ResponsablePlanta", value.ResponsablePlanta));
                                cmd.Parameters.Add(new SqlParameter("@FirmaInvetsa", value.FirmaInvetsa));
                                cmd.Parameters.Add(new SqlParameter("@FirmaPlanta", value.FirmaPlanta));
                                cmd.Parameters.Add(new SqlParameter("@FlgCerrado", value.FlgCerrado));
                                cmd.Parameters.Add(new SqlParameter("@IdUsuarioCierre", value.IdUsuarioCierre));
                                cmd.Parameters.Add(new SqlParameter("@FecCierre", value.FecCierre));
                                cmd.Parameters.Add(new SqlParameter("@EmailFrom", value.EmailFrom));
                                cmd.Parameters.Add(new SqlParameter("@EmailTo", value.EmailTo));

                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();

                                value.IdVacunacionSpray = (int)cmd.Parameters["@IdVacunacionSpray"].Value;
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                            {
                                foreach (BE_TxVacunacionSprayDetalle item in value.ListarTxVacunacionSprayDetalle)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSprayDetalle", item.IdVacunacionSprayDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSpray", value.IdVacunacionSpray));
                                    cmd.Parameters.Add(new SqlParameter("@IdProcesoDetalleSpray", item.IdProcesoDetalleSpray));
                                    cmd.Parameters.Add(new SqlParameter("@Valor", item.Valor));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_RESULTADO, conn))
                            {
                                foreach (BE_TxVacunacionSprayResultado item in value.ListarTxVacunacionSprayResultado)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSprayDetalle", item.IdVacunacionSprayDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSpray", value.IdVacunacionSpray));
                                    cmd.Parameters.Add(new SqlParameter("@IdProcesoAgrupador", item.IdProcesoAgrupador));
                                    cmd.Parameters.Add(new SqlParameter("@ValorEsperado", item.ValorEsperado));
                                    cmd.Parameters.Add(new SqlParameter("@ValorObtenido", item.ValorObtenido));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            if (value.ListarTxVacunacionSprayMaquina != null)
                            {
                                if (value.ListarTxVacunacionSprayMaquina.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_MAQUINA, conn))
                                    {
                                        foreach (BE_TxVacunacionSprayMaquina item in value.ListarTxVacunacionSprayMaquina)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdVacunacionSprayMaquina", item.IdVacunacionSprayMaquina));
                                            cmd.Parameters.Add(new SqlParameter("@IdVacunacionSpray", value.IdVacunacionSpray));
                                            cmd.Parameters.Add(new SqlParameter("@IdBoquilla", item.IdBoquilla));
                                            cmd.Parameters.Add(new SqlParameter("@NroMaquinas", item.NroMaquinas));
                                            cmd.Parameters.Add(new SqlParameter("@CodigoModelo", item.CodigoModelo));
                                            cmd.Parameters.Add(new SqlParameter("@CodigoEquipo", item.CodigoEquipo));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }
                                    
                            if (value.ListarTxVacunacionSprayVacuna != null)
                            {
                                if (value.ListarTxVacunacionSprayVacuna.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_VACUNA, conn))
                                    {
                                        foreach (BE_TxVacunacionSprayVacuna item in value.ListarTxVacunacionSprayVacuna)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdVacunacionSprayVacuna", item.IdVacunacionSprayVacuna));
                                            cmd.Parameters.Add(new SqlParameter("@IdVacunacionSpray", value.IdVacunacionSpray));
                                            cmd.Parameters.Add(new SqlParameter("@IdVacuna", item.IdVacuna));
                                            cmd.Parameters.Add(new SqlParameter("@NombreVacuna", item.NombreVacuna));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }
                                   

                            if (value.ListarTxVacunacionSprayFotos != null)
                            {
                                if (value.ListarTxVacunacionSprayFotos.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_FOTO, conn))
                                    {
                                        foreach (BE_TxVacunacionSprayFotos item in value.ListarTxVacunacionSprayFotos)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdVacunacionSprayDetalle", item.IdVacunacionSprayDetalle));
                                            cmd.Parameters.Add(new SqlParameter("@IdVacunacionSpray", value.IdVacunacionSpray));
                                            cmd.Parameters.Add(new SqlParameter("@Foto", item.Foto));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            value.IdVacunacionSpray = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdVacunacionSpray = 0;
            }

            return int.Parse(value.IdVacunacionSpray.ToString());
        }

        public async Task Update(BE_TxVacunacionSpray value)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(context.DevuelveConnectionSQL()))
                {
                    using (CommittableTransaction transaction = new CommittableTransaction())
                    {
                        await conn.OpenAsync();
                        conn.EnlistTransaction(transaction);

                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(SP_UPDATE, conn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
 
                                cmd.Parameters.Add(new SqlParameter("@IdVacunacionSpray", value.IdVacunacionSpray));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();
                            }

                            if (value.ListarTxVacunacionSprayFotos.Count() > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_FOTO, conn))
                                {
                                    foreach (BE_TxVacunacionSprayFotos item in value.ListarTxVacunacionSprayFotos)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.Add(new SqlParameter("@IdVacunacionSprayDetalle", item.IdVacunacionSprayDetalle));
                                        cmd.Parameters.Add(new SqlParameter("@IdVacunacionSpray", value.IdVacunacionSpray));
                                        cmd.Parameters.Add(new SqlParameter("@Foto", item.Foto));
                                        cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                        cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                }
                            }


                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            value.IdVacunacionSpray = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdVacunacionSpray = 0;
            }
        }
        public async Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxVacunacionSpray entidad)
        {
            BE_ResultadoTransaccion vResultadoTransaccion = new BE_ResultadoTransaccion();
            vResultadoTransaccion.ResultadoCodigo = 1;

            try
            {
                entidad.FecCierre = DateTime.Now;
                Update(entidad, SP_UPDATE_STATUS);
            }
            catch (Exception ex)
            {

                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                return vResultadoTransaccion;
            }

            var memory = new MemoryStream();
            try
            {
                var memoryPDF = await GenerarPDF(new BE_TxVacunacionSpray { IdVacunacionSpray = entidad.IdVacunacionSpray });
                memory = memoryPDF;
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                return vResultadoTransaccion;
            }


            var data = context.ExecuteSqlViewId<BE_TxVacunacionSpray>(SP_GET_ID_GOOGLE_DRIVE, new BE_TxVacunacionSpray { IdVacunacionSpray = entidad.IdVacunacionSpray });
            var nameFile = string.Format("{0}.{1}", data.NombreArchivo, "pdf");

            try
            {
                EmailSenderRepository emailSenderRepository = new EmailSenderRepository(context);
                var mensaje = string.Format("Se envía informe de Vacunación Spray - N° {0}", entidad.IdVacunacionSpray);
                await emailSenderRepository.SendEmailAsync(data.EmailTo, "Correo Automatico - Vacunación Spray", mensaje, new BE_MemoryStream { FileMemoryStream = memory }, nameFile);
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                return vResultadoTransaccion;
            }

            MemoryStream ms = memory;

            TxRegistroDocumentoRepository _repository = new TxRegistroDocumentoRepository(context);
            List<IFormFile> files = new List<IFormFile>();

            var fileMock = new Mock<IFormFile>();

            fileMock.Setup(_ => _.FileName).Returns(nameFile);
            fileMock.Setup(_ => _.ContentType).Returns("application/pdf");
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.ContentDisposition).Returns(string.Format("inline; filename={0}", nameFile));

            files.Add(fileMock.Object);

            try
            {
                var resultDocumentFile = await _repository.Create(new BE_TxRegistroDocumento
                {
                    CodigoEmpresa = data.CodigoEmpresa,
                    DescripcionEmpresa = data.DescripcionEmpresa,
                    CodigoPlanta = data.CodigoPlanta,
                    DescripcionPlanta = data.DescripcionPlanta,
                    DescripcionTipoExplotacion = data.DescripcionTipoExplotacion,
                    DescripcionSubTipoExplotacion = data.DescripcionSubTipoExplotacion,
                    IdSubTipoExplotacion = data.IdSubTipoExplotacion,
                    IdDocumento = 0,
                    IdDocumentoReferencial = (int)data.IdVacunacionSpray,
                    FlgCerrado = true,
                    FecCerrado = DateTime.Now,
                    IdUsuarioCierre = entidad.RegUsuario,
                    RegUsuario = entidad.RegUsuario,
                    RegEstacion = entidad.RegEstacion
                }, files);

                if (resultDocumentFile.ResultadoCodigo == -1)
                {
                    vResultadoTransaccion.ResultadoCodigo = -1;
                    vResultadoTransaccion.ResultadoDescripcion = resultDocumentFile.ResultadoDescripcion;
                    return vResultadoTransaccion;
                }
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                return vResultadoTransaccion;
            }

            return vResultadoTransaccion;
        }
        public Task Delete(BE_TxVacunacionSpray entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
        public async Task<MemoryStream> GenerarPDF(BE_TxVacunacionSpray entidad)
        {
            BE_TxVacunacionSpray item = await GetById(entidad);

            return await Task.Run(() =>
            {
                Document doc = new Document();
                doc.SetPageSize(PageSize.Letter);
                // points to cm
                doc.SetMargins(28.34f, 28.34f, 85f, 85f);
                MemoryStream ms = new MemoryStream();
                PdfWriter write = PdfWriter.GetInstance(doc, ms);
                doc.AddAuthor("Grupo SBA");
                doc.AddTitle("Invetsa");

                var pe = new PageEventHelper();
                pe.FlagCerrado = Boolean.Parse(item.FlgCerrado.ToString());
                write.PageEvent = pe;
                // Colocamos la fuente que deseamos que tenga el documento
                BaseFont helvetica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
                // Titulo
                iTextSharp.text.Font titulo = new iTextSharp.text.Font(helvetica, 20f, iTextSharp.text.Font.BOLD, new BaseColor(103, 93, 152));
                iTextSharp.text.Font tituloBlanco = new iTextSharp.text.Font(helvetica, 18f, iTextSharp.text.Font.NORMAL, BaseColor.White);
                iTextSharp.text.Font subTitulo = new iTextSharp.text.Font(helvetica, 14f, iTextSharp.text.Font.BOLD, new BaseColor(103, 93, 152));
                iTextSharp.text.Font parrafoBlanco = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, BaseColor.White);
                iTextSharp.text.Font parrafoNegro = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font parrafoRojo = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Red);
                pe.HeaderLeft = " ";
                pe.HeaderFont = parrafoBlanco;
                pe.HeaderRight = " ";
                doc.Open();

               

                write.Close();
                doc.Close();
                ms.Seek(0, SeekOrigin.Begin);
                var file = ms;

                return file;
            });
        }

        private iTextSharp.text.Image ImagenBase64ToImagen(string ImagenBase64, float fitWidth, float fiHeight)
        {
            Byte[] bytes;
            iTextSharp.text.Image foto = null;
            string base64Data;
            try
            {

                base64Data = ImagenBase64.Substring(ImagenBase64.IndexOf(",") + 1);
                bytes = Convert.FromBase64String(base64Data);
                foto = iTextSharp.text.Image.GetInstance(bytes);

                foto.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                foto.Border = iTextSharp.text.Rectangle.NO_BORDER;
                foto.BorderColor = iTextSharp.text.BaseColor.White;
                foto.ScaleToFit(fitWidth, fiHeight);
            }
            catch (DocumentException dex)
            {
                foto = null;
            }

            return foto;
        }

        public static Color HexToColor(string hexString)
        {
            //replace # occurences
            if (hexString.IndexOf('#') != -1)
                hexString = hexString.Replace("#", "");

            int r, g, b = 0;

            r = int.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            g = int.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            b = int.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);

            return Color.FromArgb(r, g, b);
        }

    }
}
