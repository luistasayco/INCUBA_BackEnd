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
    public class TxSINMIConsolidadoRepository : RepositoryBase<BE_TxSINMIConsolidado>, ITxSINMIConsolidadoRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_POR_FILTRO = DB_ESQUEMA + "INC_GetTxSINMIConsolidadoPorFiltros";
        const string SP_GET_POR_ID = DB_ESQUEMA + "INC_GetTxSINMIConsolidadoPorId";
        const string SP_GET_DETALLE_POR_ID = DB_ESQUEMA + "INC_GetTxSINMIConsolidadoDetallePorId";
        const string SP_GET_CONSOLIDADO_FOTOS = DB_ESQUEMA + "INC_GetTxSINMIFotosPorIdConsolidado";
        const string SP_GET_CONSOLIDADO_DETALLE = DB_ESQUEMA + "INC_GetTxSINMIConsolidadoDetallePorId";

        const string SP_GET_CONSOLIDADO_SINMI = DB_ESQUEMA + "INC_GetTxSINMIPorIdConsolidado";

        const string SP_GET_ID_GOOGLE_DRIVE = DB_ESQUEMA + "INC_GetTxSINMIPorIdGoogleDriveConsolidado";

        const string SP_UPDATE = DB_ESQUEMA + "INC_SetTxSINMIConsolidadoUpdate";
        const string SP_UPDATE_STATUS = DB_ESQUEMA + "INC_SetTxSINMIConsolidadoStatusUpdate";
        const string SP_UPDATE_DESCRIPCION = DB_ESQUEMA + "INC_SetTxSINMIConsolidadoUpdateDescripcion";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetTxSINMIConsolidadoInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetTxSINMIConsolidadoDelete";

        const string SP_MERGE_DETALLE = DB_ESQUEMA + "INC_SetTxSINMIConsolidadoDetalleMerge";

        public TxSINMIConsolidadoRepository(IConnectionSQL context)
            : base(context)
        {
        }

        public Task<IEnumerable<BE_TxSINMIConsolidado>> GetAll(FE_TxSINMIConsolidado entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxSINMIConsolidado>(SP_GET_POR_FILTRO, entidad));
        }

        public Task<BE_TxSINMIConsolidado> GetById(BE_TxSINMIConsolidado entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxSINMIConsolidado p = context.ExecuteSqlViewId<BE_TxSINMIConsolidado>(SP_GET_POR_ID, entidad);
                if (p != null)
                {
                    p.ListaTxSINMIConsolidadoDetalle = context.ExecuteSqlViewFindByCondition<BE_TxSINMIConsolidadoDetalle>(SP_GET_DETALLE_POR_ID, entidad).ToList();
                }
                else
                {
                    p = new BE_TxSINMIConsolidado();
                }
                return p;
            });
            return objListPrincipal;
        }

        public async Task<int> Create(BE_TxSINMIConsolidado value)
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

                                SqlParameter oParam = new SqlParameter("@IdSINMIConsolidado", value.IdSINMIConsolidado);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@Observacion", value.Observacion));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();

                                value.IdSINMIConsolidado = (int)cmd.Parameters["@IdSINMIConsolidado"].Value;
                            }
                            if (value.ListaTxSINMIConsolidadoDetalle != null)
                            {
                                if (value.ListaTxSINMIConsolidadoDetalle.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                                    {
                                        foreach (BE_TxSINMIConsolidadoDetalle item in value.ListaTxSINMIConsolidadoDetalle)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdSINMIConsolidadoDetalle", item.IdSINMIConsolidadoDetalle));
                                            cmd.Parameters.Add(new SqlParameter("@IdSINMIConsolidado", value.IdSINMIConsolidado));
                                            cmd.Parameters.Add(new SqlParameter("@IdSINMI", item.IdSINMI));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_UPDATE_DESCRIPCION, conn))
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.Add(new SqlParameter("@IdSINMIConsolidado", value.IdSINMIConsolidado));

                                await cmd.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            value.IdSINMIConsolidado = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdSINMIConsolidado = 0;
            }

            return int.Parse(value.IdSINMIConsolidado.ToString());
        }
        public async Task Update(BE_TxSINMIConsolidado value)
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

                                cmd.Parameters.Add(new SqlParameter("@IdSINMIConsolidado", value.IdSINMIConsolidado));
                                cmd.Parameters.Add(new SqlParameter("@Observacion", value.Observacion));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();
                            }

                            if (value.ListaTxSINMIConsolidadoDetalle.Count() > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                                {
                                    foreach (BE_TxSINMIConsolidadoDetalle item in value.ListaTxSINMIConsolidadoDetalle)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.Add(new SqlParameter("@IdSINMIConsolidadoDetalle", item.IdSINMIConsolidadoDetalle));
                                        cmd.Parameters.Add(new SqlParameter("@IdSINMIConsolidado", value.IdSINMIConsolidado));
                                        cmd.Parameters.Add(new SqlParameter("@IdSINMI", item.IdSINMI));
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
                            value.IdSINMIConsolidado = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdSINMIConsolidado = 0;
            }
        }
        public async Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxSINMIConsolidado entidad)
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
                var memoryPDF = await GenerarPDF(entidad.IdSINMIConsolidado, entidad.DescripcionEmpresa );
                memory = memoryPDF;
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                return vResultadoTransaccion;
            }

            var data = context.ExecuteSqlViewId<BE_TxSINMI>(SP_GET_ID_GOOGLE_DRIVE, new BE_TxSINMI { IdSINMIConsolidado = entidad.IdSINMIConsolidado });
            var nameFile = string.Format("{0}.{1}", data.NombreArchivo, "pdf");

            try
            {
                EmailSenderRepository emailSenderRepository = new EmailSenderRepository(context);
                var mensaje = string.Format("Se envía Consolidado del Sistema Integral de Monitoreo Intestinal - N° {0}", entidad.IdSINMIConsolidado);
                await emailSenderRepository.SendEmailAsync(data.EmailTo, "Correo Automatico - Sistema Integral de Monitoreo Intestinal", mensaje, new BE_MemoryStream { FileMemoryStream = memory }, nameFile);
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
                    IdDocumentoReferencial = (int)data.IdSINMIConsolidado,
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
        public Task Delete(BE_TxSINMIConsolidado entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
        public async Task<MemoryStream> GenerarPDF(int id, string descripcionEmpresa)
        {
            BE_TxSINMIConsolidado p = context.ExecuteSqlViewId<BE_TxSINMIConsolidado>(SP_GET_POR_ID, new BE_TxSINMIConsolidado { IdSINMIConsolidado = id });
            var listaDocumentoSINMI = context.ExecuteSqlViewFindByCondition<BE_TxSINMI>(SP_GET_CONSOLIDADO_SINMI, new BE_TxSINMIConsolidado { IdSINMIConsolidado = id }).ToList();
            var listaDetalle = context.ExecuteSqlViewFindByCondition<BE_TxSINMIDetalle>(SP_GET_CONSOLIDADO_DETALLE, new BE_TxSINMIConsolidado { IdSINMIConsolidado = id }).ToList();
            var listaFotos = context.ExecuteSqlViewFindByCondition<BE_TxSINMIFotos>(SP_GET_CONSOLIDADO_FOTOS, new BE_TxSINMIConsolidado { IdSINMIConsolidado = id }).ToList();

            return await Task.Run(() =>
            {
                Document doc = new Document();
                doc.SetPageSize(PageSize.Letter.Rotate());
                // points to cm
                doc.SetMargins(28.34f, 28.34f, 85f, 85f);
                MemoryStream ms = new MemoryStream();
                PdfWriter write = PdfWriter.GetInstance(doc, ms);
                doc.AddAuthor("Grupo SBA");
                doc.AddTitle("Invetsa");

                var pe = new PageEventHelper();
                pe.FlagCerrado = true;
                pe.FlagModulo = "SINMI";
                write.PageEvent = pe;
                // Colocamos la fuente que deseamos que tenga el documento
                BaseFont helvetica = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.WINANSI, true);
                // Titulo
                iTextSharp.text.Font titulo = new iTextSharp.text.Font(helvetica, 16f, iTextSharp.text.Font.BOLD, new BaseColor(103, 93, 152));
                iTextSharp.text.Font tituloBlanco = new iTextSharp.text.Font(helvetica, 18f, iTextSharp.text.Font.NORMAL, BaseColor.White);
                iTextSharp.text.Font subTitulo = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, new BaseColor(103, 93, 152));
                iTextSharp.text.Font subTituloParticiones = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, new BaseColor(103, 93, 152));
                iTextSharp.text.Font parrafoBlanco = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, BaseColor.White);
                iTextSharp.text.Font parrafoNegrita = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, BaseColor.Black);
                iTextSharp.text.Font parrafoNegro = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font parrafoRojo = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Red);
                pe.HeaderLeft = " ";
                pe.HeaderFont = parrafoBlanco;
                pe.HeaderRight = " ";
                doc.Open();

                var tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };

                var title = string.Format("INFORME DEL SISTEMA INTEGRAL DE MONITOREO INTESTINAL - {0}", id, titulo);
                var c1 = new PdfPCell(new Phrase(title, titulo)) { Border = 0 };
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase(p.DescripcionEmpresa, titulo)) { Border = 0 };
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegro);
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 7f, 20f, 30f, 43f}) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("LISTA DE SISTEMA INTEGRADO DE MONITOREO INTESTINAL", subTitulo);
                c1.Border = 0;
                c1.Colspan = 4;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 4;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("Edad", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Fecha/Hora", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Granja", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Motivo Visita", parrafoBlanco);
                tbl.AddCell(c1);
                foreach (BE_TxSINMI item in listaDocumentoSINMI)
                {
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(item.Edad.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.FecHoraRegistro.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.CodigoPlanta, parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.MotivoVisita, parrafoNegro);
                    tbl.AddCell(c1);
                }
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 4;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 5f, 10f, 45f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f }) { WidthPercentage = 100 };
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("Edad", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Órgano", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Observaciones", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Score", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Factor", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("AVES", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Colspan = 5;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Media", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("1", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("2", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("3", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("4", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("5", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                
                var v_DescripcionOrgano = string.Empty;

                var v_NumeroDocumento = 0;

                decimal v_Media = 0;

                decimal v_TotScoreFinal = 0;

                foreach (var detalle in listaDetalle)
                {
                    if (v_NumeroDocumento != detalle.IdSINMI)
                    {

                        if (v_NumeroDocumento > 0)
                        {
                            c1 = new PdfPCell(new Phrase("SCORE FINAL", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                            c1.HorizontalAlignment = Element.ALIGN_CENTER;
                            c1.Colspan = 9;
                            tbl.AddCell(c1);

                            c1 = new PdfPCell(new Phrase(v_TotScoreFinal.ToString("N"), parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                            c1.HorizontalAlignment = Element.ALIGN_CENTER;
                            tbl.AddCell(c1);
                        }

                        c1 = new PdfPCell(new Phrase(detalle.Edad.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        c1.Rowspan = detalle.TotalDocumento + 1;
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);

                        v_TotScoreFinal = 0;
                    }

                    if (v_DescripcionOrgano != detalle.DescripcionOrgano)
                    {
                        c1 = new PdfPCell(new Phrase(detalle.DescripcionOrgano, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        c1.Rowspan = detalle.TotalDetalle;
                        c1.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl.AddCell(c1);
                    }

                    c1 = new PdfPCell(new Phrase(detalle.DescripcionOrganoDetalle, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Score, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.FactorImpacto.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Ave1.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Ave2.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Ave3.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Ave4.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Ave5.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);

                    v_Media = 0;

                    if (detalle.FlgMedia)
                    {
                        v_Media = ((detalle.Ave1 + detalle.Ave2 + detalle.Ave3 + detalle.Ave4 + detalle.Ave5) * detalle.FactorImpacto) / 5;
                        c1 = new PdfPCell(new Phrase(v_Media.ToString("N"), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    } else
                    {
                        c1 = new PdfPCell(new Phrase("-", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    }

                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);

                    v_TotScoreFinal = v_TotScoreFinal + v_Media;

                    v_DescripcionOrgano = detalle.DescripcionOrgano;
                    v_NumeroDocumento = detalle.IdSINMI;
                }

                c1 = new PdfPCell(new Phrase("SCORE FINAL", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                c1.Colspan = 9;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase(v_TotScoreFinal.ToString("N"), parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);

                // Agamos una lina en blanco
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 10;
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("COMENTARIO", subTitulo);
                c1.Border = 0;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(p.Observacion, parrafoNegro);
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("FOTOS", subTitulo);
                c1.Border = 0;
                c1.Colspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 2;
                tbl.AddCell(c1);

                var countFotos = listaFotos.Count();
                iTextSharp.text.Image foto = null;

                foreach (BE_TxSINMIFotos item in listaFotos)
                {
                    foto = ImagenBase64ToImagen(item.Foto, 270f, 200f);

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

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 2;
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

        private iTextSharp.text.Image ImagenByteToImagen(byte[] ImagenByte, float fitWidth, float fiHeight)
        {
            iTextSharp.text.Image foto = null;
            try
            {
                foto = iTextSharp.text.Image.GetInstance(ImagenByte);

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

        //public byte[] PopulateChart(int x, int y)
        //{
        //    var chart = new Chart();
        //    var series = new Series("Employee");
        //    series.ChartType = SeriesChartType.Bar;
        //    series.Points.DataBindXY(new[] { "Peter", "Andrew", "Julie", "Mary", "Dave" }, new[] { 2, 6, 4, 5, 3 });

        //    chart.ChartAreas.Add(new ChartArea());
        //    chart.Series.Add(series);
        //    chart.SaveImage("TestChart.png", ChartImageFormat.Png);

        //}
    }
}
