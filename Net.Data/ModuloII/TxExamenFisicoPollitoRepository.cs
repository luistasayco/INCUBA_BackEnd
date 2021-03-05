using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
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
    public class TxExamenFisicoPollitoRepository : RepositoryBase<BE_TxExamenFisicoPollito>, ITxExamenFisicoPollitoRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetTxExamenFisicoPollitoPorFiltros";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetTxExamenFisicoPollitoPorId";
        const string SP_GET_ID_GOOGLE_DRIVE = DB_ESQUEMA + "INC_GetTxExamenFisicoPollitoPorIdGoogleDrive";
        const string SP_GET_DETALLE_NEW = DB_ESQUEMA + "INC_GetTxExamenFisicoPollitoDetalleNew";
        const string SP_GET_ID_DETALLE = DB_ESQUEMA + "INC_GetTxExamenFisicoPollitoDetallePorId";
        const string SP_GET_ID_DETALLE_FOTOS = DB_ESQUEMA + "INC_GetTxExamenFisicoPollitoDetalleFotosPorId";
        const string SP_GET_ID_RESUMEN = DB_ESQUEMA + "INC_GetTxExamenFisicoPollitoResumenPorId";

        const string SP_INSERT = DB_ESQUEMA + "INC_SetTxExamenFisicoPollitoInsert";
        const string SP_MERGE_DETALLE = DB_ESQUEMA + "INC_SetTxExamenFisicoPollitoDetalleMerge";
        const string SP_MERGE_DETALLE_FOTO = DB_ESQUEMA + "INC_SetTxExamenFisicoPollitoDetalleFotosMerge";
        const string SP_MERGE_DETALLE_RESUMEN = DB_ESQUEMA + "INC_SetTxExamenFisicoPollitoResumenMerge";

        const string SP_UPDATE_STATUS = DB_ESQUEMA + "INC_SetTxExamenFisicoPollitoUpdateStatus";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetTxExamenFisicoPollitoDelete";

        public TxExamenFisicoPollitoRepository(IConnectionSQL context)
            : base(context)
        {
        }

        public Task<IEnumerable<BE_TxExamenFisicoPollito>> GetAll(FE_TxExamenFisicoPollito entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxExamenFisicoPollito>(SP_GET, entidad));
        }

        public Task<BE_TxExamenFisicoPollito> GetById(BE_TxExamenFisicoPollito entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxExamenFisicoPollito p = context.ExecuteSqlViewId<BE_TxExamenFisicoPollito>(SP_GET_ID, entidad);
                if (p != null)
                {
                    p.ListDetalleNew = context.ExecuteSqlViewFindByCondition<BE_TxExamenFisicoPollitoDetalleNew>(SP_GET_ID_DETALLE, entidad).ToList();
                    p.ListDetalleFotos = context.ExecuteSqlViewFindByCondition<BE_TxExamenFisicoPollitoDetalleFotos>(SP_GET_ID_DETALLE_FOTOS, entidad).ToList();
                    p.ListDetalleResumen = context.ExecuteSqlViewFindByCondition<BE_TxExamenFisicoPollitoResumen>(SP_GET_ID_RESUMEN, entidad).ToList();
                }
                else
                {
                    p = new BE_TxExamenFisicoPollito();
                }
                return p;
            });
            return objListPrincipal;
        }

        private BE_TxExamenFisicoPollito GetByIdPDF(BE_TxExamenFisicoPollito entidad)
        {
            BE_TxExamenFisicoPollito p = context.ExecuteSqlViewId<BE_TxExamenFisicoPollito>(SP_GET_ID, entidad);
            p.ListDetalleFotos = context.ExecuteSqlViewFindByCondition<BE_TxExamenFisicoPollitoDetalleFotos>(SP_GET_ID_DETALLE_FOTOS, entidad).ToList();
            p.ListDetalleResumen = context.ExecuteSqlViewFindByCondition<BE_TxExamenFisicoPollitoResumen>(SP_GET_ID_RESUMEN, entidad).ToList();
            return p;
        }

        public Task<IEnumerable<BE_TxExamenFisicoPollitoDetalleNew>> GetByDetalleNew(BE_TxExamenFisicoPollito entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxExamenFisicoPollitoDetalleNew>(SP_GET_DETALLE_NEW, entidad));
        }

        public Task<BE_TxExamenFisicoPollito> GetByIdNew(BE_TxExamenFisicoPollito entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxExamenFisicoPollito p = new BE_TxExamenFisicoPollito();
                p.IdExamenFisico = 0;
                p.IdCalidad = 0;
                p.LineaGenetica = string.Empty;
                p.Lote = string.Empty;
                p.ListDetalleNew = context.ExecuteSqlViewFindByCondition<BE_TxExamenFisicoPollitoDetalleNew>(SP_GET_DETALLE_NEW, entidad).ToList();
                return p;
            });
            return objListPrincipal;
        }

        private Boolean ValidaById(BE_TxExamenFisicoPollito entidad)
        {
            var vExiste = false;
            BE_TxExamenFisicoPollito p = context.ExecuteSqlViewId<BE_TxExamenFisicoPollito>(SP_GET_ID, entidad);
            if (p == null)
            {
                vExiste = false;
            }
            else
            {
                vExiste = true;
            }
            return vExiste;
        }

        public async Task<int> Create(BE_TxExamenFisicoPollito value)
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

                                SqlParameter oParam = new SqlParameter("@IdExamenFisico", value.IdExamenFisico);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@CodigoEmpresa", value.CodigoEmpresa));
                                cmd.Parameters.Add(new SqlParameter("@CodigoPlanta", value.CodigoPlanta));
                                cmd.Parameters.Add(new SqlParameter("@FecRegistro", value.FecRegistro));
                                cmd.Parameters.Add(new SqlParameter("@ResponsableInvetsa", value.ResponsableInvetsa));
                                cmd.Parameters.Add(new SqlParameter("@ResponsablePlanta", value.ResponsablePlanta));
                                cmd.Parameters.Add(new SqlParameter("@NumeroNacedora", value.NumeroNacedora));
                                cmd.Parameters.Add(new SqlParameter("@Lote", value.Lote));
                                cmd.Parameters.Add(new SqlParameter("@PesoPromedio", value.PesoPromedio));
                                cmd.Parameters.Add(new SqlParameter("@EdadReproductora", value.EdadReproductora));
                                cmd.Parameters.Add(new SqlParameter("@Sexo", value.Sexo));
                                cmd.Parameters.Add(new SqlParameter("@LineaGenetica", value.LineaGenetica));
                                cmd.Parameters.Add(new SqlParameter("@Calificacion", value.Calificacion));
                                cmd.Parameters.Add(new SqlParameter("@Uniformidad", value.Uniformidad));
                                cmd.Parameters.Add(new SqlParameter("@IdCalidad", value.IdCalidad));
                                cmd.Parameters.Add(new SqlParameter("@FirmaInvetsa", value.FirmaInvetsa));
                                cmd.Parameters.Add(new SqlParameter("@FirmaPlanta", value.FirmaPlanta));
                                cmd.Parameters.Add(new SqlParameter("@EmailFrom", value.EmailFrom));
                                cmd.Parameters.Add(new SqlParameter("@EmailTo", value.EmailTo));
                                cmd.Parameters.Add(new SqlParameter("@FlgCerrado", value.FlgCerrado));
                                cmd.Parameters.Add(new SqlParameter("@IdUsuarioCierre", value.IdUsuarioCierre));
                                cmd.Parameters.Add(new SqlParameter("@FecCierre", value.FecCierre));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();

                                value.IdExamenFisico = (int)cmd.Parameters["@IdExamenFisico"].Value;
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                            {
                                foreach (BE_TxExamenFisicoPollitoDetalleNew item in value.ListDetalleNew)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdExamenFisicoDetalle", item.IdExamenFisicoDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdExamenFisico", value.IdExamenFisico));
                                    cmd.Parameters.Add(new SqlParameter("@NumeroPollito", item.NumeroPollito));
                                    if (item.Factor == 0)
                                    {
                                        cmd.Parameters.Add(new SqlParameter("@IdProcesoDetalle", item.IdProcesoDetalle));
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add(new SqlParameter("@IdProcesoDetalle", item.Valor));
                                    }
                                    cmd.Parameters.Add(new SqlParameter("@Valor", item.Valor));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_RESUMEN, conn))
                            {
                                foreach (BE_TxExamenFisicoPollitoResumen item in value.ListDetalleResumen)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdExamenFisicoDetalle", item.IdExamenFisicoDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdExamenFisico", value.IdExamenFisico));
                                    cmd.Parameters.Add(new SqlParameter("@IdProceso", item.IdProceso));
                                    cmd.Parameters.Add(new SqlParameter("@Esperado", item.Esperado));
                                    cmd.Parameters.Add(new SqlParameter("@Obtenido", item.Obtenido));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            if (value.ListDetalleFotos != null)
                            {
                                if (value.ListDetalleFotos.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_FOTO, conn))
                                    {
                                        foreach (BE_TxExamenFisicoPollitoDetalleFotos item in value.ListDetalleFotos)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdExamenFisicoDetalle", item.IdExamenFisicoDetalle));
                                            cmd.Parameters.Add(new SqlParameter("@IdExamenFisico", value.IdExamenFisico));
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
                            value.IdExamenFisico = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdExamenFisico = 0;
            }

            return int.Parse(value.IdExamenFisico.ToString());
        }

        public async Task Update(BE_TxExamenFisicoPollito value)
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
                            if (value.ListDetalleFotos.Count() > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_FOTO, conn))
                                {
                                    foreach (BE_TxExamenFisicoPollitoDetalleFotos item in value.ListDetalleFotos)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.Add(new SqlParameter("@IdExamenFisicoDetalle", item.IdExamenFisicoDetalle));
                                        cmd.Parameters.Add(new SqlParameter("@IdExamenFisico", value.IdExamenFisico));
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
                            value.IdExamenFisico = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdExamenFisico = 0;
            }
        }
        public async Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxExamenFisicoPollito entidad)
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
                var memoryPDF = await GenerarPDF(new BE_TxExamenFisicoPollito { IdExamenFisico = entidad.IdExamenFisico });
                memory = memoryPDF;
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                return vResultadoTransaccion;
            }

            

            //Obtiene informacion del examen fisicion del pollito bebe
            var data = context.ExecuteSqlViewId<BE_TxExamenFisicoPollito>(SP_GET_ID_GOOGLE_DRIVE, new BE_TxExamenFisicoPollito { IdExamenFisico = entidad.IdExamenFisico });
            var nameFile = string.Format("{0}.{1}", data.NombreArchivo, "pdf");

            try
            {
                EmailSenderRepository emailSenderRepository = new EmailSenderRepository(context);
                var mensaje = string.Format("Se envía informe de Examen Físico de Pollito BB - N° {0}", entidad.IdExamenFisico);
                await emailSenderRepository.SendEmailAsync(data.EmailTo, "Correo Automatico - Examen Físico de Pollito BB", mensaje, new BE_MemoryStream { FileMemoryStream = memory }, nameFile);
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
                    IdDocumentoReferencial = (int)data.IdExamenFisico,
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
        public Task Delete(BE_TxExamenFisicoPollito entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
        public async Task<MemoryStream> GenerarPDF(BE_TxExamenFisicoPollito entidad)
        {
            BE_TxExamenFisicoPollito item = GetByIdPDF(entidad);
            CalidadRepository ICalidad = new CalidadRepository(context);
            IEnumerable<BE_Calidad> calidad = ICalidad.GetAllCalidad(new BE_Calidad { Descripcion = "" });

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

                var tblTitulo = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };

                var title = string.Format("EXÁMEN FÍSICO DE POLLITO BB - {0}", entidad.IdExamenFisico, titulo);
                var c1Titulo = new PdfPCell(new Phrase(title, titulo)) { Border = 0};
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
                c1.Phrase = new Phrase("Responsable:", parrafoNegro);
                c2.Phrase = new Phrase(item.ResponsablePlanta, parrafoNegro);
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
                c1.Phrase = new Phrase("EVALUACIÓN FÍSICA DE CALIDAD DE POLLO BB", tituloBlanco);
                c1.BackgroundColor = new BaseColor(103, 93, 152);
                c1.PaddingBottom = 8f;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 15f, 15f, 20f, 15f, 15f, 20f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 6;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Lote:", parrafoNegro);
                c1.Border = 0;
                c1.BackgroundColor = new BaseColor(211,211,211);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.Lote, parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Edad Reproductora", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.EdadReproductora, parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Nacedora N°:", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(int.Parse(item.NumeroNacedora.ToString()).ToString("N"), parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Línea Genética:", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.LineaGenetica, parrafoNegro);
                c1.Colspan = 5;
                tbl.AddCell(c1);

                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 6;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 20f, 15f, 75f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Pollitos muestra", parrafoNegro);
                c1.BackgroundColor = new BaseColor(211, 211, 211);
                c1.Padding = 6f;
                tbl.AddCell(c1);
                c2 = new PdfPCell();
                c2.Phrase = new Phrase("100", parrafoNegro);
                c2.HorizontalAlignment = Element.ALIGN_CENTER;
                c2.Padding = 6f;
                tbl.AddCell(c2);
                c3 = new PdfPCell();
                c3.Phrase = new Phrase(" ", parrafoNegro);
                c3.Border = 0;
                tbl.AddCell(c3);
                
                c1.Phrase = new Phrase("Peso Promedio (g)", parrafoNegro);
                tbl.AddCell(c1);
                c2.Phrase = new Phrase(decimal.Parse(item.PesoPromedio.ToString()).ToString("N"), parrafoNegro);
                tbl.AddCell(c2);
                c3.Phrase = new Phrase(" ", parrafoNegro);
                tbl.AddCell(c3);

                c1.Phrase = new Phrase("Uniformidad (%)", parrafoNegro);
                tbl.AddCell(c1);
                c2.Phrase = new Phrase(int.Parse(item.Uniformidad.ToString()).ToString("N"), parrafoNegro);
                tbl.AddCell(c2);
                c3.Phrase = new Phrase(" ", parrafoNegro);
                tbl.AddCell(c3);

                c1.Phrase = new Phrase("Sexo", parrafoNegro);
                tbl.AddCell(c1);
                c2.Phrase = new Phrase(item.Sexo, parrafoNegro);
                tbl.AddCell(c2);
                c3.Phrase = new Phrase(" ", parrafoNegro);
                tbl.AddCell(c3);

                // Agamos una lina en blanco
                c3.Phrase = new Phrase(" ", parrafoNegro);
                c3.Colspan = 3;
                tbl.AddCell(c3);

                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 47f, 6f, 47f }) { WidthPercentage = 100f };
                var tbl1 = new PdfPTable(new float[] { 50f, 25f, 25f }) { WidthPercentage = 100f };
                var tbl2 = new PdfPTable(new float[] { 50f, 25f, 25f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Criterio", parrafoNegro);
                c1.BackgroundColor = new BaseColor(211, 211, 211);
                c1.Padding = 6f;
                tbl1.AddCell(c1);
                tbl2.AddCell(c1);
                c2 = new PdfPCell();
                c2.Phrase = new Phrase("Esperado", parrafoNegro);
                c2.HorizontalAlignment = Element.ALIGN_CENTER;
                c2.Padding = 6f;
                tbl1.AddCell(c2);
                tbl2.AddCell(c2);
                c3 = new PdfPCell();
                c3.Phrase = new Phrase("Obtenido", parrafoNegro);
                c3.HorizontalAlignment = Element.ALIGN_CENTER;
                c3.Padding = 6f;
                tbl1.AddCell(c3);
                tbl2.AddCell(c3);

                int i = 1;
                foreach (BE_TxExamenFisicoPollitoResumen itemResumen in item.ListDetalleResumen)
                {
                    c1.Phrase = new Phrase(itemResumen.DescripcionProceso, parrafoNegro);
                   
                    c2.Phrase = new Phrase(itemResumen.Esperado.ToString("N"), parrafoNegro);
                    if (itemResumen.Obtenido < itemResumen.Esperado)
                    {
                        c3.Phrase = new Phrase(itemResumen.Obtenido.ToString("N"), parrafoRojo);
                    }
                    else
                    {
                        c3.Phrase = new Phrase(itemResumen.Obtenido.ToString("N"), parrafoNegro);
                    }
                    

                    if ((i % 2) != 0)
                    {
                        tbl1.AddCell(c1);
                        tbl1.AddCell(c2);
                        tbl1.AddCell(c3);
                    } else
                    {
                        tbl2.AddCell(c1);
                        tbl2.AddCell(c2);
                        tbl2.AddCell(c3);
                    }

                    i = i + 1;
                }

                tbl.AddCell(tbl1);
                c3 = new PdfPCell();
                c3.Border = 0;
                tbl.AddCell(c3);
                tbl.AddCell(tbl2);

                c3.Phrase = new Phrase(" ", parrafoNegro);
                c3.Colspan = 3;
                tbl.AddCell(c3);

                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 20f, 15f, 75f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Calificación", parrafoNegro);
                c1.BackgroundColor = new BaseColor(211, 211, 211);
                c1.Padding = 6f;
                tbl.AddCell(c1);
                c2 = new PdfPCell();
                c2.Phrase = new Phrase(decimal.Parse(item.Calificacion.ToString()).ToString("N"), parrafoNegro);
                c2.HorizontalAlignment = Element.ALIGN_CENTER;
                c2.Padding = 6f;
                tbl.AddCell(c2);
                c3 = new PdfPCell();
                c3.Phrase = new Phrase(" ", parrafoNegro);
                c3.Border = 0;
                tbl.AddCell(c3);

                c1.Phrase = new Phrase("Resultado Calidad", parrafoNegro);
                c1.Padding = 6f;
                tbl.AddCell(c1);
                c2.Phrase = new Phrase(item.DescripcionCalidad, parrafoNegro);
                c2.BackgroundColor = new BaseColor(HexToColor(item.ColorCalidad));
                c2.Padding = 6f;
                tbl.AddCell(c2);
                c3.Phrase = new Phrase(" ", parrafoNegro);
                tbl.AddCell(c3);

                // Agamos una lina en blanco
                c3.Phrase = new Phrase(" ", parrafoNegro);
                c3.Colspan = 3;
                tbl.AddCell(c3);

                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 20f, 25f, 55f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Calificación", parrafoNegro);
                c1.BackgroundColor = new BaseColor(211, 211, 211);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                c1.Padding = 6f;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Calidad de pollitos", parrafoNegro);
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                tbl.AddCell(c1);

                foreach (BE_Calidad itemCalidad in calidad)
                {
                    c1 = new PdfPCell();
                    var rango = string.Format("{0} - {1}", itemCalidad.RangoInicial, itemCalidad.RangoFinal);
                    c1.Phrase = new Phrase(rango, parrafoNegro);
                    c1.BackgroundColor = new BaseColor(HexToColor(itemCalidad.Color));
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    c1.Padding = 4f;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemCalidad.Descripcion, parrafoNegro);
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(" ", parrafoNegro);
                    c1.Border = 0;
                    tbl.AddCell(c1);
                }

                // Agamos una lina en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 3;
                tbl.AddCell(c1);
                // Agamos una lina en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 3;
                tbl.AddCell(c1);
                // Agamos una lina en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 3;
                tbl.AddCell(c1);

                doc.Add(tbl);

                // Validamos si ingresaron imagenes

                var countFotos = item.ListDetalleFotos.Count();
                iTextSharp.text.Image foto = null;

                if (countFotos > 0)
                {
                    //doc.Add(new Phrase("\n"));

                    doc.Add(new Phrase("Fotos", subTitulo));

                    doc.Add(new Phrase(" "));

                    tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f };
                    c1 = new PdfPCell();

                    foreach (var detalle7 in item.ListDetalleFotos)
                    {
                        foto = ImagenBase64ToImagen(detalle7.Foto, 270f, 200f);

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
