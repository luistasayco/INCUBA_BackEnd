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
    public class TxSINMIRepository : RepositoryBase<BE_TxSINMI>, ITxSINMIRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_POR_FILTRO = DB_ESQUEMA + "INC_GetTxSINMIPorFiltros";
        const string SP_GET_POR_CONSOLIDADO = DB_ESQUEMA + "INC_GetTxSINMIPorCodigoEmpresa";
        const string SP_GET_POR_ID = DB_ESQUEMA + "INC_GetTxSINMIPorId";
        const string SP_GET_POR_ID_DETALLE = DB_ESQUEMA + "INC_GetTxSINMIDetallePorId";
        const string SP_GET_POR_ID_FOTO = DB_ESQUEMA + "INC_GetTxSINMIFotosPorId";

        const string SP_GET_DETALLE_NEW = DB_ESQUEMA + "INC_GetTxSINMIPorIdNew";

        const string SP_GET_ID_GOOGLE_DRIVE = DB_ESQUEMA + "INC_GetTxSINMIPorIdGoogleDrive";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetTxSINMIUpdate";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetTxSINMInsert";
        const string SP_UPDATE_STATUS = DB_ESQUEMA + "INC_SetTxSINMIUpdateStatus";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetTxSINMIDelete";

        const string SP_MERGE_DETALLE = DB_ESQUEMA + "INC_SetTxSINMIDetalleMerge";
        const string SP_MERGE_FOTO = DB_ESQUEMA + "INC_SetTxSINMIFotosMerge";

        public TxSINMIRepository(IConnectionSQL context)
            : base(context)
        {
        }

        public Task<IEnumerable<BE_TxSINMI>> GetAll(FE_TxSINMI entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxSINMI>(SP_GET_POR_FILTRO, entidad));
        }

        public Task<IEnumerable<BE_TxSINMI>> GetByCodigoEmpresa(string codigoEmpresa)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxSINMI>(SP_GET_POR_CONSOLIDADO, new FE_TxSINMIConsolidadoCodigo { CodigoEmpresa = codigoEmpresa }));
        }

        public Task<BE_TxSINMI> GetById(BE_TxSINMI entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxSINMI p = context.ExecuteSqlViewId<BE_TxSINMI>(SP_GET_POR_ID, entidad);
                if (p != null)
                {
                    p.ListaTxSINMIDetalle = context.ExecuteSqlViewFindByCondition<BE_TxSINMIDetalle>(SP_GET_POR_ID_DETALLE, entidad).ToList();
                    p.ListaTxSINMIFotos = context.ExecuteSqlViewFindByCondition<BE_TxSINMIFotos>(SP_GET_POR_ID_FOTO, entidad).ToList();
                }
                else
                {
                    p = new BE_TxSINMI();
                }
                return p;
            });
            return objListPrincipal;
        }
        public Task<IEnumerable<BE_TxSINMIDetalle>> GetAllDetalleNew()
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxSINMIDetalle>(SP_GET_DETALLE_NEW, new FE_TxSINMI { IdSINMI = 0}));
        }
        private BE_TxSINMI GetByIdPDF(BE_TxSINMI entidad)
        {
            BE_TxSINMI p = context.ExecuteSqlViewId<BE_TxSINMI>(SP_GET_POR_ID, entidad);
            p.ListaTxSINMIDetalle = context.ExecuteSqlViewFindByCondition<BE_TxSINMIDetalle>(SP_GET_POR_ID_DETALLE, entidad).ToList();
            p.ListaTxSINMIFotos = context.ExecuteSqlViewFindByCondition<BE_TxSINMIFotos>(SP_GET_POR_ID_FOTO, entidad).ToList();
            return p;
        }

        public async Task<int> Create(BE_TxSINMI value)
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

                                SqlParameter oParam = new SqlParameter("@IdSINMI", value.IdSINMI);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@CodigoEmpresa", value.CodigoEmpresa));
                                cmd.Parameters.Add(new SqlParameter("@CodigoPlanta", value.CodigoPlanta));
                                cmd.Parameters.Add(new SqlParameter("@Edad", value.Edad));
                                cmd.Parameters.Add(new SqlParameter("@MotivoVisita", value.MotivoVisita));
                                cmd.Parameters.Add(new SqlParameter("@FecRegistro", value.FecRegistro));
                                cmd.Parameters.Add(new SqlParameter("@ResponsableInvetsa", value.ResponsableInvetsa));
                                cmd.Parameters.Add(new SqlParameter("@ResponsableIncubadora", value.ResponsableIncubadora));
                                cmd.Parameters.Add(new SqlParameter("@ObservacionInvetsa", value.ObservacionInvetsa));
                                cmd.Parameters.Add(new SqlParameter("@ObservacionPlanta", value.ObservacionPlanta));
                                cmd.Parameters.Add(new SqlParameter("@FlgCerrado", value.FlgCerrado));
                                cmd.Parameters.Add(new SqlParameter("@IdUsuarioCierre", value.IdUsuarioCierre));
                                cmd.Parameters.Add(new SqlParameter("@FecCierre", value.FecCierre));
                                cmd.Parameters.Add(new SqlParameter("@EmailFrom", value.EmailFrom));
                                cmd.Parameters.Add(new SqlParameter("@EmailTo", value.EmailTo));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();

                                value.IdSINMI = (int)cmd.Parameters["@IdSINMI"].Value;
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                            {
                                foreach (BE_TxSINMIDetalle item in value.ListaTxSINMIDetalle)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdSINMIDetalle", item.IdSINMIDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdSINMI", value.IdSINMI));
                                    cmd.Parameters.Add(new SqlParameter("@IdOrganoDetalle", item.IdOrganoDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@Ave1", item.Ave1));
                                    cmd.Parameters.Add(new SqlParameter("@Ave2", item.Ave2));
                                    cmd.Parameters.Add(new SqlParameter("@Ave3", item.Ave3));
                                    cmd.Parameters.Add(new SqlParameter("@Ave4", item.Ave4));
                                    cmd.Parameters.Add(new SqlParameter("@Ave5", item.Ave5));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                            if (value.ListaTxSINMIFotos != null)
                            {
                                if (value.ListaTxSINMIFotos.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_FOTO, conn))
                                    {
                                        foreach (BE_TxSINMIFotos item in value.ListaTxSINMIFotos)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdSINMIFoto", item.IdSINMIFoto));
                                            cmd.Parameters.Add(new SqlParameter("@IdSINMI", value.IdSINMI));
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
                            value.IdSINMI = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdSINMI = 0;
            }

            return int.Parse(value.IdSINMI.ToString());
        }

        public async Task Update(BE_TxSINMI value)
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

                                cmd.Parameters.Add(new SqlParameter("@IdSINMI", value.IdSINMI));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();
                            }

                            if (value.ListaTxSINMIFotos.Count() > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_MERGE_FOTO, conn))
                                {
                                    foreach (BE_TxSINMIFotos item in value.ListaTxSINMIFotos)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.Add(new SqlParameter("@IdSINMIFoto", item.IdSINMIFoto));
                                        cmd.Parameters.Add(new SqlParameter("@IdSINMI", value.IdSINMI));
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
                            value.IdSINMI = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdSINMI = 0;
            }
        }
        public async Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxSINMI entidad)
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

            return vResultadoTransaccion;
        }
        public Task Delete(BE_TxSINMI entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
        public async Task<MemoryStream> GenerarPDF(BE_TxSINMI entidad)
        {
            BE_TxSINMI item = GetByIdPDF(entidad);

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

                var tblTitulo = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };

                var title = string.Format("SISTEMA INTEGRADO DE MONITOREO INTESTINAL - {0}", entidad.IdSINMI, titulo);
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
                var c4 = new PdfPCell(new Phrase("Granja:", parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c5 = new PdfPCell(new Phrase(item.CodigoPlanta, parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                c1.Phrase = new Phrase("Edad:", parrafoNegro);
                c2.Phrase = new Phrase(item.Edad.ToString(), parrafoNegro);
                c3.Phrase = new Phrase("", parrafoNegro);
                c4.Phrase = new Phrase("Fecha:", parrafoNegro);
                c5.Phrase = new Phrase(item.FecHoraRegistro.ToString(), parrafoNegro);
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                c1.Phrase = new Phrase("Motivo Visita:", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.MotivoVisita, parrafoNegro);
                c1.Colspan = 4;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 5;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 10f, 33f,  7f, 10f, 10f, 10f, 10f, 10f }) { WidthPercentage = 100 };
                c1 = new PdfPCell();

                c1 = new PdfPCell(new Phrase("Órgano", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Observaciones", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Score", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("AVES", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Colspan = 5;
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

                foreach (var detalle in item.ListaTxSINMIDetalle)
                {
                    
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

                    v_DescripcionOrgano = detalle.DescripcionOrgano;
                }
                // Agamos una lina en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 8;
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("OBSERVACIONES", subTitulo);
                c1.PaddingBottom = 8f;
                c1.Border = 0;
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
                c1.Phrase = new Phrase("", parrafoNegro);
                tbl.AddCell(c1);
                doc.Add(tbl);

                var countFotos = item.ListaTxSINMIFotos.Count();
                iTextSharp.text.Image foto = null;

                if (countFotos > 0)
                {

                    tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f };
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase("FOTOS", subTitulo);
                    c1.Colspan = 2;
                    c1.Border = 0;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase("", subTitulo);
                    c1.Colspan = 2;
                    c1.Border = 0;
                    tbl.AddCell(c1);
                    foreach (var detalle7 in item.ListaTxSINMIFotos)
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

                c1 = new PdfPCell(new Phrase("Responsable de Planta", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Border = 0;
                //c1.BorderWidthTop = 2f;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase(" "));
                c1.Border = 0;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("Responsable de Invetsa", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Border = 0;
                //c1.BorderWidthTop = 2f;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("Nombre: " + item.ResponsableIncubadora, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
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
