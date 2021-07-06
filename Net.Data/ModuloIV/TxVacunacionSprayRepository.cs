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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Net.Data
{
    public class TxVacunacionSprayRepository : RepositoryBase<BE_TxVacunacionSpray>, ITxVacunacionSprayRepository
    {
        private string _aplicacionName;
        private string _metodoName;
        private readonly Regex regex = new Regex(@"<(\w+)>.*");

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
            _aplicacionName = this.GetType().Name;
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

        public async Task<BE_ResultadoTransaccion> Create(BE_TxVacunacionSpray value)
        {
            BE_ResultadoTransaccion vResultadoTransaccion = new BE_ResultadoTransaccion();
            _metodoName = regex.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Groups[1].Value.ToString();

            vResultadoTransaccion.ResultadoMetodo = _metodoName;
            vResultadoTransaccion.ResultadoAplicacion = _aplicacionName;

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

                            if (value.ListarTxVacunacionSprayDetalle != null)
                            {
                                if (value.ListarTxVacunacionSprayDetalle.Any())
                                {
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
                                }
                            }

                            if (value.ListarTxVacunacionSprayResultado != null)
                            {
                                if (value.ListarTxVacunacionSprayResultado.Any())
                                {

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
                                }
                            }

                            if (value.ListarTxVacunacionSprayMaquina != null)
                            {
                                if (value.ListarTxVacunacionSprayMaquina.Any())
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
                            vResultadoTransaccion.IdRegistro = (int)value.IdVacunacionSpray;
                            vResultadoTransaccion.ResultadoCodigo = 0;
                            vResultadoTransaccion.ResultadoDescripcion = "Se realizo correctamente";
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            vResultadoTransaccion.ResultadoCodigo = -1;
                            vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                            return vResultadoTransaccion;
                        }
                    }
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
                iTextSharp.text.Font subTitulo = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD,  BaseColor.White);
                iTextSharp.text.Font parrafoBlanco = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, BaseColor.White);
                iTextSharp.text.Font parrafoNegrita = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, BaseColor.Black);
                iTextSharp.text.Font parrafoNegro = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font parrafoRojo = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Red);
                pe.HeaderLeft = " ";
                pe.HeaderFont = parrafoBlanco;
                pe.HeaderRight = " ";
                doc.Open();

                var tblTitulo = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };

                var title = string.Format("VACUNACIÓN SPRAY - {0}", entidad.IdVacunacionSpray, titulo);
                var c1Titulo = new PdfPCell(new Phrase(title, titulo)) { Border = 0 };
                c1Titulo.HorizontalAlignment = Element.ALIGN_CENTER;
                c1Titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
                tblTitulo.AddCell(c1Titulo);
                doc.Add(tblTitulo);

                doc.Add(new Phrase(" "));
                doc.Add(new Phrase(" "));

                var tbl = new PdfPTable(new float[] { 12f, 37f, 4f, 20f, 27f }) { WidthPercentage = 100 };

                var c1 = new PdfPCell(new Phrase("Compañia:", parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c2 = new PdfPCell(new Phrase(item.DescripcionEmpresa, parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c3 = new PdfPCell(new Phrase("", parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c4 = new PdfPCell(new Phrase("Unidad - Planta:", parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c5 = new PdfPCell(new Phrase(item.DescripcionPlanta.ToString(), parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                c1.Phrase = new Phrase("Fecha:", parrafoNegro);
                c2.Phrase = new Phrase(DateTime.Parse(item.FecRegistro.ToString()).ToShortDateString(), parrafoNegro);
                c3.Phrase = new Phrase("", parrafoNegro);
                c4.Phrase = new Phrase("Hora:", parrafoNegro);
                c5.Phrase = new Phrase(DateTime.Parse(item.FecHoraRegistro.ToString()).ToShortTimeString(), parrafoNegro);
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                c1.Phrase = new Phrase("Responsable Incubadora:", parrafoNegro);
                c2.Phrase = new Phrase(item.ResponsableIncubadora, parrafoNegro);
                c3.Phrase = new Phrase("", parrafoNegro);
                c4.Phrase = new Phrase("Responsable Invetsa:", parrafoNegro);
                c5.Phrase = new Phrase(item.ResponsableInvetsa, parrafoNegro);
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 5;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };
                c1.Phrase = new Phrase("INFORMACIÓN GENERAL", tituloBlanco);
                c1.BackgroundColor = new BaseColor(103, 93, 152);
                c1.PaddingBottom = 8f;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);


                tbl = new PdfPTable(new float[] { 30f, 10f, 10f, 10f, 10f, 10f, 20f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Línea Genética:", parrafoNegro);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("HY LINE",  Boolean.Parse(item.FlgHyLine.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("LOHMAN", Boolean.Parse(item.FlgLohman.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("COBB", Boolean.Parse(item.FlgCobb.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("ROSS", Boolean.Parse(item.FlgRoss.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("OTROS:", Boolean.Parse(item.FlgOtros.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.ObservacionOtros, parrafoNegro);
                tbl.AddCell(c1);

                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 30f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 20f , 15f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("N° de nacimientos/semana:", parrafoNegro);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("L", Boolean.Parse(item.FlgLunes.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("M", Boolean.Parse(item.FlgMartes.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("M", Boolean.Parse(item.FlgMiercoles.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("J", Boolean.Parse(item.FlgJueves.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("V", Boolean.Parse(item.FlgViernes.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("S", Boolean.Parse(item.FlgSabado.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("D", Boolean.Parse(item.FlgDomingo.ToString()) ? parrafoNegrita : parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Pollos/dia(promedio)", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.PromedioPollos.ToString(), parrafoNegro);
                tbl.AddCell(c1);

                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 10;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };
                c1.Phrase = new Phrase("VACUNACIÓN", subTitulo);
                c1.BackgroundColor = new BaseColor(103, 93, 152);
                //c1.PaddingBottom = 8f;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };
                c1.Phrase = new Phrase("SPRAY", subTitulo);
                c1.BackgroundColor = new BaseColor(211, 211, 211);
                //c1.PaddingBottom = 8f;
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 15f, 15f, 55f, 15f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Máquinas", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("N° Máquinas", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Detalle modelo/marca", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Código AF", parrafoNegro);
                tbl.AddCell(c1);

                foreach (BE_TxVacunacionSprayMaquina itemMaquina in item.ListarTxVacunacionSprayMaquina)
                {
                    c1.Phrase = new Phrase(itemMaquina.DescripcionBoquilla, parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemMaquina.NroMaquinas.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemMaquina.DescripcionModelo, parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemMaquina.CodigoEquipo, parrafoNegro);
                    tbl.AddCell(c1);
                }
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Vacunas empleadas en la vacunación", subTitulo);
                c1.BackgroundColor = new BaseColor(103, 93, 152);
                //c1.PaddingBottom = 8f;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 25f, 75f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Vacuna", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Nombre Vacuna", parrafoNegro);
                tbl.AddCell(c1);

                if (item.ListarTxVacunacionSprayVacuna != null)
                {
                    foreach (BE_TxVacunacionSprayVacuna itemVacuna in item.ListarTxVacunacionSprayVacuna)
                    {
                        c1.Phrase = new Phrase(itemVacuna.DescripcionVacuna, parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(itemVacuna.NombreVacuna, parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl.AddCell(c1);
                    }
                }
                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 2;
                tbl.AddCell(c1);
                doc.Add(tbl);

                var proceso = "";
                var countDetalleNroQuiebreDos = 0;
                tbl = new PdfPTable(new float[] { 30f, 10f, 10f, 30f, 10f, 10f }) { WidthPercentage = 100f };

                foreach (BE_TxVacunacionSprayDetalle itemDetalle in item.ListarTxVacunacionSprayDetalle.Where(x => x.NroQuiebre == 2))
                {
                    if (proceso != itemDetalle.DescripcionProcesoSpray)
                    {
                        if ((countDetalleNroQuiebreDos % 2) != 0 && proceso != "")
                        {
                            c1 = new PdfPCell();
                            c1.Phrase = new Phrase("", parrafoNegro);
                            c1.BackgroundColor = new BaseColor(211, 211, 211);
                            tbl.AddCell(c1);
                            c1.Phrase = new Phrase("", parrafoNegro);
                            c1.BackgroundColor = new BaseColor(211, 211, 211);
                            tbl.AddCell(c1);
                            c1.Phrase = new Phrase("", parrafoNegro);
                            tbl.AddCell(c1);
                        }

                        c1 = new PdfPCell();
                        c1.Phrase = new Phrase(itemDetalle.DescripcionProcesoSpray, subTitulo);
                        c1.BackgroundColor = new BaseColor(103, 93, 152);
                        c1.Colspan = 6;
                        c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tbl.AddCell(c1);

                        c1 = new PdfPCell();
                        c1.Phrase = new Phrase("ESPERADO", parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase("SI", parrafoNegro);
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase("NO", parrafoNegro);
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase("ESPERADO", parrafoNegro);
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase("SI", parrafoNegro);
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase("NO", parrafoNegro);
                        tbl.AddCell(c1);

                        countDetalleNroQuiebreDos = 0;
                    }

                    var segunCondicionFormatoLeta = parrafoNegro;

                    if (!itemDetalle.Valor)
                    {
                        segunCondicionFormatoLeta = parrafoRojo;
                    }

                    c1.Phrase = new Phrase(itemDetalle.DescripcionProcesoDetalleSpray, segunCondicionFormatoLeta);
                    c1.HorizontalAlignment = Element.ALIGN_MIDDLE;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemDetalle.Valor ? "X" : "", segunCondicionFormatoLeta);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemDetalle.Valor ? "" : "X", segunCondicionFormatoLeta);
                    tbl.AddCell(c1);

                    proceso = itemDetalle.DescripcionProcesoSpray;
                    countDetalleNroQuiebreDos = countDetalleNroQuiebreDos + 1;
                }
                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 6;
                tbl.AddCell(c1);
                doc.Add(tbl);

                proceso = "";

                tbl = new PdfPTable(new float[] { 90f, 10f }) { WidthPercentage = 100f };

                foreach (BE_TxVacunacionSprayDetalle itemDetalle in item.ListarTxVacunacionSprayDetalle.Where(x => x.NroQuiebre == 1))
                {
                    if (proceso != itemDetalle.DescripcionProcesoSpray)
                    {
                        c1 = new PdfPCell();
                        c1.Phrase = new Phrase(itemDetalle.DescripcionProcesoSpray, subTitulo);
                        c1.BackgroundColor = new BaseColor(103, 93, 152);
                        c1.PaddingBottom = 8f;
                        c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        c1.Colspan = 2;
                        tbl.AddCell(c1);
                        c1 = new PdfPCell();
                        c1.Phrase = new Phrase("Asignar si el procedimiento estuviese siendo seguido, puntaje máximo " + itemDetalle.ValorProcesoSpray.ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase("Puntaje", parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);

                    }

                    c1.Phrase = new Phrase(itemDetalle.DescripcionProcesoDetalleSpray, parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemDetalle.Valor ? itemDetalle.ValorProcesoDetalleSpray.ToString() : "", parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);

                    proceso = itemDetalle.DescripcionProcesoSpray;
                }
                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 2;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 70f, 15f, 15f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("PUNTAJE TOTAL OBTENIDO", subTitulo);
                c1.BackgroundColor = new BaseColor(103, 93, 152);
                c1.PaddingBottom = 8f;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                c1.Colspan = 3;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("PUNTOS DE CONTROL", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("ESPERADO", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("ALCANZADO", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);

                decimal vTotalValorEsperado = 0;
                decimal vTotalValorObtenido = 0;

                foreach (BE_TxVacunacionSprayResultado itemResultado in item.ListarTxVacunacionSprayResultado)
                {
                    c1.Phrase = new Phrase(itemResultado.DescripcionProcesoAgrupador, parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemResultado.ValorEsperado.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemResultado.ValorObtenido.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);

                    vTotalValorEsperado += itemResultado.ValorEsperado;
                    vTotalValorObtenido += itemResultado.ValorObtenido;
                }
                // Insertamos los totales
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(vTotalValorEsperado.ToString(), parrafoNegro);
                c1.BackgroundColor = new BaseColor(184, 182, 181);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(vTotalValorObtenido.ToString(), parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);

                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 3;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("RECOMENDACIONES, FOTOS Y OBSERVACIONES", subTitulo);
                c1.BackgroundColor = new BaseColor(103, 93, 152);
                c1.PaddingBottom = 8f;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);

                doc.Add(tbl);


                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Observaciones Invetsa:", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.ObservacionInvetsa, parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Observaciones Planta:", parrafoNegrita);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.ObservacionPlanta, parrafoNegro);
                tbl.AddCell(c1);
                doc.Add(tbl);

                // Validamos si ingresaron imagenes

                var countFotos = item.ListarTxVacunacionSprayFotos.Count();
                iTextSharp.text.Image foto = null;


                if (countFotos > 0)
                {

                    tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f };
                    c1 = new PdfPCell();

                    foreach (BE_TxVacunacionSprayFotos itemFoto in item.ListarTxVacunacionSprayFotos)
                    {
                        foto = ImagenBase64ToImagen(itemFoto.Foto, 270f, 200f);

                        if (foto != null)
                        {
                            c1 = new PdfPCell(foto);
                            c1.HorizontalAlignment = Element.ALIGN_CENTER;
                            c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            c1.Border = 0;
                            tbl.AddCell(c1);
                        }
                    }

                    if ((countFotos % 2) != 0)
                    {
                        c1 = new PdfPCell(new Phrase(" "));
                        c1.Border = 0;
                        tbl.AddCell(c1);
                    }

                    doc.Add(tbl);
                }

                doc.Add(new Phrase(" "));

                tbl = new PdfPTable(new float[] { 45f, 10f, 45f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();

                foto = ImagenBase64ToImagen(item.FirmaPlanta, 270f, 100f);

                c1 = new PdfPCell(foto);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                c1.Border = 0;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase(" "));
                c1.Border = 0;
                tbl.AddCell(c1);

                foto = ImagenBase64ToImagen(item.FirmaInvetsa, 270f, 100f);

                c1 = new PdfPCell(foto);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                c1.Border = 0;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("Responsable de Planta", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Border = 0;
                c1.BorderWidthTop = 2f;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase(" "));
                c1.Border = 0;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("Responsable de Invetsa", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Border = 0;
                c1.BorderWidthTop = 2f;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("Nombre: " + item.ResponsablePlanta, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Border = 0;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase(" "));
                c1.Border = 0;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("Nombre: " + item.ResponsableInvetsa, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Border = 0;
                tbl.AddCell(c1);

                doc.Add(tbl);

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
