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
    public class TxSIMConsolidadoRepository : RepositoryBase<BE_TxSIMConsolidado>, ITxSIMConsolidadoRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_POR_FILTRO = DB_ESQUEMA + "INC_GetTxSIMConsolidadoPorFiltros";
        const string SP_GET_POR_ID = DB_ESQUEMA + "INC_GetTxSIMConsolidadoPorId";
        const string SP_GET_DETALLE_POR_ID = DB_ESQUEMA + "INC_GetTxSIMConsolidadoDetallePorId";

        const string SP_GET_CONSOLIDADO_SIM = DB_ESQUEMA + "INC_GetTxSIMPorIdConsolidado";
        const string SP_GET_CONSOLIDADO_INDICE_BURSAL = DB_ESQUEMA + "INC_GetTxSIMIndiceBursalPorIdConsolidado";
        const string SP_GET_CONSOLIDADO_LESION_BURSAL = DB_ESQUEMA + "INC_GetTxSIMLesionBursaPorIdConsolidado";
        const string SP_GET_CONSOLIDADO_LESION_TIMO = DB_ESQUEMA + "INC_GetTxSIMLesionTimoPorIdConsolidado";
        const string SP_GET_CONSOLIDADO_LESIONES = DB_ESQUEMA + "INC_GetTxSIMLesionesPorIdConsolidado";
        const string SP_GET_CONSOLIDADO_FOTOS = DB_ESQUEMA + "INC_GetTxSIMFotosPorIdConsolidado";

        const string SP_GET_ID_GOOGLE_DRIVE = DB_ESQUEMA + "INC_GetTxSIMPorIdGoogleDriveConsolidado";

        const string SP_UPDATE = DB_ESQUEMA + "INC_SetTxSIMConsolidadoUpdate";
        const string SP_UPDATE_STATUS = DB_ESQUEMA + "INC_SetTxSIMConsolidadoStatusUpdate";
        const string SP_UPDATE_DESCRIPCION = DB_ESQUEMA + "INC_SetTxSIMConsolidadoUpdateDescripcion";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetTxSIMConsolidadoInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetTxSIMConsolidadoDelete";

        const string SP_MERGE_DETALLE = DB_ESQUEMA + "INC_SetTxSIMConsolidadoDetalleMerge";

        public TxSIMConsolidadoRepository(IConnectionSQL context)
            : base(context)
        {
        }

        public Task<IEnumerable<BE_TxSIMConsolidado>> GetAll(FE_TxSIMConsolidado entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxSIMConsolidado>(SP_GET_POR_FILTRO, entidad));
        }

        public Task<BE_TxSIMConsolidado> GetById(BE_TxSIMConsolidado entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxSIMConsolidado p = context.ExecuteSqlViewId<BE_TxSIMConsolidado>(SP_GET_POR_ID, entidad);
                if (p != null)
                {
                    p.ListaTxSIMConsolidadoDetalle = context.ExecuteSqlViewFindByCondition<BE_TxSIMConsolidadoDetalle>(SP_GET_DETALLE_POR_ID, entidad).ToList();
                }
                else
                {
                    p = new BE_TxSIMConsolidado();
                }
                return p;
            });
            return objListPrincipal;
        }



        public async Task<int> Create(BE_TxSIMConsolidado value)
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

                                SqlParameter oParam = new SqlParameter("@IdSIMConsolidado", value.IdSIMConsolidado);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@Observacion", value.Observacion));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();

                                value.IdSIMConsolidado = (int)cmd.Parameters["@IdSIMConsolidado"].Value;
                            }
                            if (value.ListaTxSIMConsolidadoDetalle != null)
                            {
                                if (value.ListaTxSIMConsolidadoDetalle.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                                    {
                                        foreach (BE_TxSIMConsolidadoDetalle item in value.ListaTxSIMConsolidadoDetalle)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdSIMConsolidadoDetalle", item.IdSIMConsolidadoDetalle));
                                            cmd.Parameters.Add(new SqlParameter("@IdSIMConsolidado", value.IdSIMConsolidado));
                                            cmd.Parameters.Add(new SqlParameter("@IdSIM", item.IdSIM));
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

                                cmd.Parameters.Add(new SqlParameter("@IdSIMConsolidado", value.IdSIMConsolidado));

                                await cmd.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            value.IdSIMConsolidado = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdSIMConsolidado = 0;
            }

            return int.Parse(value.IdSIMConsolidado.ToString());
        }

        public async Task Update(BE_TxSIMConsolidado value)
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

                                cmd.Parameters.Add(new SqlParameter("@IdSIMConsolidado", value.IdSIMConsolidado));
                                cmd.Parameters.Add(new SqlParameter("@Observacion", value.Observacion));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();
                            }

                            if (value.ListaTxSIMConsolidadoDetalle.Count() > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                                {
                                    foreach (BE_TxSIMConsolidadoDetalle item in value.ListaTxSIMConsolidadoDetalle)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.Add(new SqlParameter("@IdSIMConsolidadoDetalle", item.IdSIMConsolidadoDetalle));
                                        cmd.Parameters.Add(new SqlParameter("@IdSIMConsolidado", value.IdSIMConsolidado));
                                        cmd.Parameters.Add(new SqlParameter("@IdSIM", item.IdSIM));
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
                            value.IdSIMConsolidado = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdSIMConsolidado = 0;
            }
        }

        public async Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxSIMConsolidado entidad)
        {
            BE_ResultadoTransaccion vResultadoTransaccion = new BE_ResultadoTransaccion();
            vResultadoTransaccion.ResultadoCodigo = 1;

            try
            {
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
                var memoryPDF = await GenerarPDF(entidad.IdSIMConsolidado, entidad.DescripcionEmpresa);
                memory = memoryPDF;
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                return vResultadoTransaccion;
            }



            //Obtiene informacion del examen fisicion del pollito bebe
            var data = context.ExecuteSqlViewId<BE_TxSIM>(SP_GET_ID_GOOGLE_DRIVE, new BE_TxSIM { IdSIMConsolidado = entidad.IdSIMConsolidado });
            var nameFile = string.Format("{0}.{1}", data.NombreArchivo, "pdf");

            try
            {
                EmailSenderRepository emailSenderRepository = new EmailSenderRepository(context);
                var mensaje = string.Format("Se envía Consolidado del Sistema Integral de Monitoreo de Campo - N° {0}", entidad.IdSIMConsolidado);
                await emailSenderRepository.SendEmailAsync(data.EmailTo, "Correo Automatico - Sistema Integral de Monitoreo de Campo", mensaje, new BE_MemoryStream { FileMemoryStream = memory }, nameFile);
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
                    IdDocumentoReferencial = (int)data.IdSIMConsolidado,
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
        public Task Delete(BE_TxSIMConsolidado entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }

        public async Task<MemoryStream> GenerarPDF(int id, string descripcionEmpresa)
        {
            BE_TxSIMConsolidado p = context.ExecuteSqlViewId<BE_TxSIMConsolidado>(SP_GET_POR_ID, new BE_TxSIMConsolidado { IdSIMConsolidado = id });

            var listaDocumentoSIM = context.ExecuteSqlViewFindByCondition<BE_TxSIM>(SP_GET_CONSOLIDADO_SIM, new BE_TxSIMConsolidado { IdSIMConsolidado = id }).ToList();

            var listaIndiceBursal = context.ExecuteSqlViewFindByCondition<BE_TxSIMIndiceBursal>(SP_GET_CONSOLIDADO_INDICE_BURSAL, new BE_TxSIMConsolidado { IdSIMConsolidado = id }).ToList();

            var listaLesionBursa = context.ExecuteSqlViewFindByCondition<BE_TxSIMLesionBursa>(SP_GET_CONSOLIDADO_LESION_BURSAL, new BE_TxSIMConsolidado { IdSIMConsolidado = id }).ToList();

            var listaLesionTimo = context.ExecuteSqlViewFindByCondition<BE_TxSIMLesionTimo>(SP_GET_CONSOLIDADO_LESION_TIMO, new BE_TxSIMConsolidado { IdSIMConsolidado = id }).ToList();

            var listaLesiones = context.ExecuteSqlViewFindByCondition<BE_TxSIMLesiones>(SP_GET_CONSOLIDADO_LESIONES, new BE_TxSIMConsolidado { IdSIMConsolidado = id }).ToList();

            var listaFotos = context.ExecuteSqlViewFindByCondition<BE_TxSIMFotos>(SP_GET_CONSOLIDADO_FOTOS, new BE_TxSIMConsolidado { IdSIMConsolidado = id }).ToList();

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
                pe.FlagModulo = "SIM";
                write.PageEvent = pe;
                // Colocamos la fuente que deseamos que tenga el documento
                BaseFont helvetica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
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

                var title = string.Format("INFORME DEL SISTEMA INTEGRADO DE MONITOREO DE CAMPO - {0}", id, titulo);
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

                tbl = new PdfPTable(new float[] { 7f, 20f, 30f, 29f, 7f, 7f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("LISTA DE SISTEMA INTEGRADO DE MONITOREO DE CAMPO", subTitulo);
                c1.Border = 0;
                c1.Colspan = 6;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 6;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("Edad", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Fecha/Hora", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Granja", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Zona", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Galpón", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Nro Pollos", parrafoBlanco);
                tbl.AddCell(c1);
                foreach (BE_TxSIM item in listaDocumentoSIM)
                {
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(item.Edad.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.FecHoraRegistro.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.CodigoPlanta, parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.Zona, parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.Galpon.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.NroPollos.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                }
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 6;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 7f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 13f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("I.ÍNDICE DE ÓRGANOS LINFOIDES", subTitulo);
                c1.Border = 0;
                c1.Colspan = 10;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 10;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("Edad", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Peso Corporal (g)", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Peso de Bursa (g)", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Peso de Bazo (g)", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Peso de Timo (g)", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Peso de Hígado (g)", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Índice Bursal", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Índice Timico", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Índice Hepático", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Bursómetro", parrafoBlanco);
                tbl.AddCell(c1);
                foreach (BE_TxSIMIndiceBursal item in listaIndiceBursal)
                {
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(item.Edad.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(((int)item.PesoCorporal).ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.PesoBursa.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.PesoBazo.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.PesoTimo.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.PesoHigado.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.IndiceBursal.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.IndiceTimico.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.IndiceHepatico.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.Bursometro.ToString(), parrafoNegro);
                    tbl.AddCell(c1);
                }
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 10;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 10f, 15f, 15f, 15f, 15f, 15f, 15f}) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("II.	SCORE DE LESIONES HISTOPATOLÓGICAS DE BURSA Y TIMO", subTitulo);
                c1.Border = 0;
                c1.Colspan = 7;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 7;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("EDAD", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("SCORE DE LESIÓN DE BURSA", parrafoBlanco);
                c1.Colspan = 5;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("PROMEDIO", parrafoBlanco);
                tbl.AddCell(c1);

                int v_Edad = 0;

                foreach (BE_TxSIMLesionBursa item in listaLesionBursa)
                {
                    c1 = new PdfPCell();
                    if (v_Edad != item.Edad)
                    {
                        c1.Phrase = new Phrase(item.Edad.ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                    }

                    c1.Phrase = new Phrase(item.Valor.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);

                    v_Edad = item.Edad;
                }

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 7;
                tbl.AddCell(c1);

                
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("EDAD", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("SCORE DE LESIÓN DE TIMO", parrafoBlanco);
                c1.Colspan = 5;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("PROMEDIO", parrafoBlanco);
                tbl.AddCell(c1);

                v_Edad = 0;

                foreach (BE_TxSIMLesionTimo item in listaLesionTimo)
                {
                    c1 = new PdfPCell();
                    if (v_Edad != item.Edad)
                    {
                        c1.Phrase = new Phrase(item.Edad.ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                    }

                    c1.Phrase = new Phrase(item.Valor.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);

                    v_Edad = item.Edad;
                }
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 7;
                tbl.AddCell(c1);

                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 10f, 30f, 30f, 30f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("III.	HALLAZGO DE LESIONES HISTOPATOLÓGICAS DE ÓRGANOS", subTitulo);
                c1.Border = 0;
                c1.Colspan = 4;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 4;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("EDAD", parrafoBlanco)) { BackgroundColor = new BaseColor(51, 153, 68), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("LESIONES DE DUODENO", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("LESIONES EN INSTESTINO MEDIO", parrafoBlanco);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("LESIONES EN HÍGADO", parrafoBlanco);
                tbl.AddCell(c1);

                foreach (BE_TxSIMLesiones item in listaLesiones)
                {
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(item.Edad.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.LesionesDeudemo, parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.LesionesIntestinoMedio, parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(item.LesionesHigado, parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_LEFT;
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
                c1.Phrase = new Phrase("IV. COMENTARIO", subTitulo);
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
                c1.Phrase = new Phrase("V. FOTOS", subTitulo);
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

                foreach (BE_TxSIMFotos item in listaFotos)
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
