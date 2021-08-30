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
    public class TxSINMIConsolidadoRepository : RepositoryBase<BE_TxSINMIConsolidado>, ITxSINMIConsolidadoRepository
    {
        private string _aplicacionName;
        private string _metodoName;
        private readonly Regex regex = new Regex(@"<(\w+)>.*");

        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_POR_FILTRO = DB_ESQUEMA + "INC_GetTxSINMIConsolidadoPorFiltros";
        const string SP_GET_POR_ID = DB_ESQUEMA + "INC_GetTxSINMIConsolidadoPorId";
        const string SP_GET_DETALLE_POR_ID = DB_ESQUEMA + "INC_GetTxSINMIPorIdConsolidado";
        const string SP_GET_CONSOLIDADO_FOTOS = DB_ESQUEMA + "INC_GetTxSINMIFotosPorIdConsolidado";
        const string SP_GET_CONSOLIDADO_DETALLE = DB_ESQUEMA + "INC_GetTxSINMIConsolidadoDetallePorId";

        const string SP_GET_CONSOLIDADO_RESULTADO = DB_ESQUEMA + "INC_GetTxSINMIConsolidadoResultadoPorId";

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
            _aplicacionName = this.GetType().Name;
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
        public async Task<BE_ResultadoTransaccion> Create(BE_TxSINMIConsolidado value)
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

                                SqlParameter oParam = new SqlParameter("@IdSINMIConsolidado", value.IdSINMIConsolidado);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@Conclusion", value.Conclusion));
                                cmd.Parameters.Add(new SqlParameter("@Resultado", value.Resultado));
                                cmd.Parameters.Add(new SqlParameter("@Linea", value.Linea));
                                cmd.Parameters.Add(new SqlParameter("@PersonaContacto", value.PersonaContacto));
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
                            vResultadoTransaccion.IdRegistro = (int)value.IdSINMIConsolidado;
                            vResultadoTransaccion.ResultadoCodigo = 0;
                            vResultadoTransaccion.ResultadoDescripcion = string.Format("Se registro correctamente");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            vResultadoTransaccion.IdRegistro = -1;
                            vResultadoTransaccion.ResultadoCodigo = -1;
                            vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.IdRegistro = -1;
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
            }

            return vResultadoTransaccion;
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
                                cmd.Parameters.Add(new SqlParameter("@Conclusion", value.Conclusion));
                                cmd.Parameters.Add(new SqlParameter("@Resultado", value.Resultado));
                                cmd.Parameters.Add(new SqlParameter("@Linea", value.Linea));
                                cmd.Parameters.Add(new SqlParameter("@PersonaContacto", value.PersonaContacto));
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
            var listaResultado = context.ExecuteSqlViewFindByCondition<BE_TxSINMIResultado>(SP_GET_CONSOLIDADO_RESULTADO, new BE_TxSINMIConsolidado { IdSINMIConsolidado = id }).ToList();
            var listaDetalle = context.ExecuteSqlViewFindByCondition<BE_TxSINMIDetalle>(SP_GET_CONSOLIDADO_DETALLE, new BE_TxSINMIConsolidado { IdSINMIConsolidado = id }).ToList();
            var listaFotos = context.ExecuteSqlViewFindByCondition<BE_TxSINMIFotos>(SP_GET_CONSOLIDADO_FOTOS, new BE_TxSINMIConsolidado { IdSINMIConsolidado = id }).ToList();
            
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
                pe.FlagCerrado = true;
                pe.FlagModulo = "SINMI";
                write.PageEvent = pe;
                // Colocamos la fuente que deseamos que tenga el documento
                BaseFont helvetica = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.WINANSI, true);
                // Titulo
                iTextSharp.text.Font titulo = new iTextSharp.text.Font(helvetica, 20f, iTextSharp.text.Font.BOLD, new BaseColor(103, 93, 152));
                iTextSharp.text.Font tituloBlanco = new iTextSharp.text.Font(helvetica, 18f, iTextSharp.text.Font.NORMAL, BaseColor.White);
                iTextSharp.text.Font subTitulo = new iTextSharp.text.Font(helvetica, 14f, iTextSharp.text.Font.BOLD, new BaseColor(103, 93, 152));
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

                var title = string.Format("SINMI - {1} - CONSOLIDADO - {0}", id, p.DescripcionEmpresa ,titulo);
                var c1 = new PdfPCell(new Phrase(title, titulo)) { Border = 0 };
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);

                doc.Add(new Phrase(" "));
                doc.Add(new Phrase(" "));

                tbl = new PdfPTable(new float[] { 22f, 40f, 4f, 10f, 24f }) { WidthPercentage = 100 };

                c1 = new PdfPCell(new Phrase("Compañia:", parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c2 = new PdfPCell(new Phrase(p.DescripcionEmpresa, parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c3 = new PdfPCell(new Phrase("", parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c4 = new PdfPCell(new Phrase("Linea:", parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c5 = new PdfPCell(new Phrase(p.Linea, parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                c1.Phrase = new Phrase("Persona contacto:", parrafoNegro);
                c2.Phrase = new Phrase(p.PersonaContacto, parrafoNegro);
                c3.Phrase = new Phrase("", parrafoNegro);
                c4.Phrase = new Phrase("Fecha:", parrafoNegro);
                c5.Phrase = new Phrase(p.FecHoraRegistro.ToString(), parrafoNegro);
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Colspan = 4;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 5;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 7f, 24f, 26f, 43f}) { WidthPercentage = 100f };
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

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("A. TOTAL GENERAL SALUD INTESTINAL", subTitulo);
                c1.PaddingBottom = 8f;
                c1.Border = 0;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);

                int numeroEdades = 0;
                
                
                foreach (var item in listaResultado)
                {
                    if (item.Proceso == "ScoreFinal")
                    {
                        numeroEdades++;
                    }
                }

                float[] anchoCelda = new float[numeroEdades+2];

                for (int i = 0; i < anchoCelda.Length; i++)
                {
                    if (i == 0 || i == anchoCelda.Length - 1) anchoCelda[i] = 18f;
                    else anchoCelda[i] = 54f/numeroEdades;
                }

                tbl = new PdfPTable(anchoCelda) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Edad días", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Colspan = numeroEdades;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Score General Salud Intestinal", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68),HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);

                List<double> edadList = new List<double>();
                List<double> edadList1 = new List<double>();
                List<double> scoreList = new List<double>();
               
                List<double> indiceList = new List<double>();

                //
                foreach (var item in listaResultado)
                {
                    if (item.Proceso == "ScoreFinal")
                    {
                        c1 = new PdfPCell(new Phrase(item.Edad.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        edadList.Add(item.Edad);
                    }
                }

                //
                c1 = new PdfPCell(new Phrase("Score final", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                //
                decimal scoreTotal = 0M;
                int contador = 0;
                foreach (var item in listaResultado)
                {
                    if (item.Proceso == "ScoreFinal")
                    {
                        scoreTotal += item.ScoreFinal;
                        contador++;
                        c1 = new PdfPCell(new Phrase(item.ScoreFinal.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        scoreList.Add(Decimal.ToDouble(item.ScoreFinal));
                    }
                }
                scoreTotal = decimal.Round((scoreTotal / contador),2);
                //
                c1 = new PdfPCell(new Phrase(scoreTotal.ToString(), parrafoBlanco)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, BackgroundColor = new BaseColor(51, 153, 68) };
                tbl.AddCell(c1);
                c1.BackgroundColor = null;

                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 8;
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();

            
                edadList.Add(1000);
                
                scoreList.Add(Decimal.ToDouble(scoreTotal));

                var plt = new ScottPlot.Plot(400, 300);

                double[] edades = edadList.ToArray();
                double[] values = scoreList.ToArray();
                string[] labels = new string[edadList.Count];
                double[] positions = new double[edadList.Count];

                for (int i = 0; i < labels.Length; i++)
                {
                    if (edades[i]==1000)
                    {
                        labels[i] = "Score Total";
                    }
                    else labels[i] = Math.Round(edades[i], 0).ToString() + " dias";
                    positions[i] = i+1;
                }
                

                var bar = plt.AddBar(values,positions);
                plt.XTicks(positions, labels);
                bar.BarWidth = (positions[1] - positions[0]) * .8;
                bar.ShowValuesAboveBars = true;
                plt.SaveFig("img/quickstart.png");

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Path.Combine(Environment.CurrentDirectory, "img", "quickstart.png"));

                c1 = new PdfPCell(img) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Padding = 2f;
                tbl.AddCell(c1);

                // Hagamos una linea en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 8;
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("B. TOTAL ÍNDICE HEPÁTICO", subTitulo);
                c1.PaddingBottom = 8f;
                c1.Border = 0;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(anchoCelda) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Edad días", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Colspan = numeroEdades;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Esperado", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68),HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                //
                foreach (var item in listaResultado)
                {
                    if (item.Proceso == "ScoreFinal")
                    {
                        c1 = new PdfPCell(new Phrase(item.Edad.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        edadList1.Add(item.Edad);
                    }
                }
                //
                c1 = new PdfPCell(new Phrase("Índice hepático", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                //
                foreach (var item in listaResultado)
                {
                    if (item.Proceso == "ScoreFinal")
                    {
                        c1 = new PdfPCell(new Phrase(item.IndiceHepatico.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        indiceList.Add(Decimal.ToDouble(item.IndiceHepatico));
                        
                    }
                }
                //
                c1 = new PdfPCell(new Phrase("<=3.0", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68),HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                //doc.Add(tbl);
                c1.BackgroundColor = null;
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 8;
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();

                double[] edades1 = edadList1.ToArray();
                string[] labels1 = new string[edadList1.Count];
                double[] positions1 = new double[edadList1.Count];
                double[] values1 = indiceList.ToArray();
                double[] errors = new double[edadList1.Count];

                for (int i = 0; i < labels1.Length; i++)
                {
                    labels1[i] = Math.Round(edades1[i], 0).ToString() + " dias";
                    positions1[i] = i + 1;

                }

                var plt1 = new ScottPlot.Plot(400, 300);    
                var bar1 = plt1.AddBar(values1, positions1);
                plt1.XTicks(positions1, labels1);
                //bar1.BarWidth = (positions1[1] - positions1[0]) * .8;
                
                bar1.ShowValuesAboveBars = true;
                var line = plt1.AddHorizontalLine(3,Color.Red);
                line.LineWidth = 3;
                plt1.SaveFig("img/quickstart1.png");

                img = iTextSharp.text.Image.GetInstance(Path.Combine(Environment.CurrentDirectory, "img", "quickstart1.png"));

                c1 = new PdfPCell(img) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Padding = 2f;
                tbl.AddCell(c1);

                // Hagamos una linea en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 8;
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("C. SCORE DE LESIONES POR COCCIDIA" , subTitulo);
                c1.PaddingBottom = 8f;
                c1.Border = 0;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);

                int cantidadLesiones = 1;
                decimal sumaLesiones = 0;
                decimal[] especie = new decimal[3];

                decimal duodeno;
                decimal yeyuno;
                decimal ciego;
                string especieEimeria;

                foreach (var item in listaResultado)
                {
                    
                    if (item.Proceso == "ScoreLesiones")
                    {
                        
                        if (cantidadLesiones == 1)
                        {
                            tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                            c1 = new PdfPCell();
                            c1.Phrase = new Phrase("Edad: "+item.Edad+" dias", subTituloParticiones);
                            c1.PaddingBottom = 8f;
                            c1.Border = 0;
                            c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            tbl.AddCell(c1);
                            doc.Add(tbl);

                            tbl = new PdfPTable(new float[] { 30f, 10f, 10f, 10f, 10f, 10f, 20f }) { WidthPercentage = 100f };
                            tbl.HorizontalAlignment = Element.ALIGN_LEFT;
                            c1 = new PdfPCell();

                            c1 = new PdfPCell(new Phrase("Aves", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            tbl.AddCell(c1);
                            c1 = new PdfPCell(new Phrase("1", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            tbl.AddCell(c1);
                            c1 = new PdfPCell(new Phrase("2", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            tbl.AddCell(c1);
                            c1 = new PdfPCell(new Phrase("3", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68),HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            tbl.AddCell(c1);
                            c1 = new PdfPCell(new Phrase("4", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            tbl.AddCell(c1);
                            c1 = new PdfPCell(new Phrase("5", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            tbl.AddCell(c1);
                            c1 = new PdfPCell(new Phrase("Total", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            tbl.AddCell(c1);
                            
                        }


                        c1 = new PdfPCell(new Phrase(item.ProcesoDetalle, parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        c1 = new PdfPCell(new Phrase(item.Ave1.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        c1 = new PdfPCell(new Phrase(item.Ave2.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        c1 = new PdfPCell(new Phrase(item.Ave3.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        c1 = new PdfPCell(new Phrase(item.Ave4.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        c1 = new PdfPCell(new Phrase(item.Ave5.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        c1 = new PdfPCell(new Phrase(item.Total.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(c1);
                        sumaLesiones += item.Total;
                        especie[cantidadLesiones - 1] = item.Total;

                        cantidadLesiones++;

                        if (cantidadLesiones == 4)
                        {
                            c1 = new PdfPCell(new Phrase("Suma", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68),HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            c1.Colspan = 6;
                            tbl.AddCell(c1);

                            c1 = new PdfPCell(new Phrase(sumaLesiones.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            tbl.AddCell(c1);

                            c1 = new PdfPCell(new Phrase("Especie predominante*", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            c1.Colspan = 2;
                            tbl.AddCell(c1);

                            duodeno = especie[0];
                            yeyuno = especie[1];
                            ciego = especie[2];
                            especieEimeria = "";

                            Array.Sort(especie);

                            if (especie[2] == duodeno) especieEimeria += " E. acervulina ";
                            if (especie[2] == yeyuno) especieEimeria += " E. máxima ";
                            if (especie[2] == ciego) especieEimeria += " E. tenella ";

                            c1 = new PdfPCell(new Phrase(especieEimeria, parrafoNegro)) { BackgroundColor = new BaseColor(57, 255, 20),HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            c1.Colspan = 3;
                            tbl.AddCell(c1);
                            c1 = new PdfPCell(new Phrase("Score", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
 
                            tbl.AddCell(c1);
                            c1 = new PdfPCell(new Phrase((sumaLesiones/15).ToString(), parrafoNegro)) { BackgroundColor = new BaseColor(57, 255, 20), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                            tbl.AddCell(c1);
                            // Agamos una lina en blanco
                            c1 = new PdfPCell();
                            c1.Phrase = new Phrase(" ", parrafoNegro);
                            c1.Colspan = 10;
                            c1.Border = 0;
                            tbl.AddCell(c1);
                            doc.Add(tbl);
                            cantidadLesiones = 1;
                        }
                        
                    }

                    
                }

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Score esperado según tipo de anticoccidial", subTitulo);
                c1.PaddingBottom = 8f;
                c1.Border = 0;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);

                //Tabla Especie predominante Eimeria
                tbl = new PdfPTable(new float[] { 20f, 20f }) { WidthPercentage = 40f };
                tbl.HorizontalAlignment = Element.ALIGN_LEFT;
                c1 = new PdfPCell();

                c1 = new PdfPCell(new Phrase("Ionóforo", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Score", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68),HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("Normal", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68),HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("0.2 a 0.6", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("Coccidiosis", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase(">0.8", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 10;
                c1.Border = 0;
                tbl.AddCell(c1);
                //doc.Add(tbl);

                c1 = new PdfPCell(new Phrase("Químico", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68),HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Score", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68),HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("Normal", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("<0.2", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("Coccidiosis", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68),HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase(">0.2", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
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
                c1.Phrase = new Phrase("RESULTADOS", subTitulo);
                c1.PaddingBottom = 8f;
                c1.Border = 0;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Border = 0;
                c1.VerticalAlignment = Element.ALIGN_JUSTIFIED_ALL;
                c1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(p.Resultado, parrafoNegro);
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("CONCLUSIONES", subTitulo);
                c1.PaddingBottom = 8f;
                c1.PaddingTop = 8f;
                c1.Border = 0;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);               
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Border = 0;
                c1.VerticalAlignment = Element.ALIGN_JUSTIFIED_ALL;
                c1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(p.Conclusion, parrafoNegro);
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

        /*public byte[] PopulateChart(int x, int y)
        {
            var chart = new Chart();
            var series = new Series("Employee");
            series.ChartType = SeriesChartType.Bar;
            series.Points.DataBindXY(new[] { "Peter", "Andrew", "Julie", "Mary", "Dave" }, new[] { 2, 6, 4, 5, 3 });

            chart.ChartAreas.Add(new ChartArea());
            chart.Series.Add(series);
            chart.SaveImage("TestChart.png", ChartImageFormat.Png);

        }*/
    }
}
