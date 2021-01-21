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
    public class TxVacunacionSubCutaneaRepository : RepositoryBase<BE_TxVacunacionSubCutanea>, ITxVacunacionSubCutaneaRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaPorFiltros";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaPorId";
        const string SP_GET_ID_GOOGLE_DRIVE = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaPorIdGoogleDrive";
        const string SP_GET_DETALLE_NEW = DB_ESQUEMA + "";
        const string SP_GET_ID_DETALLE = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaDetallePorId";
        const string SP_GET_ID_DETALLE_FOTOS = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaFotosPorId";
        const string SP_GET_ID_DETALLE_MAQUINA = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaMaquinaPorId";
        const string SP_GET_ID_DETALLE_VACUNA = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaVacunaPorId";
        const string SP_GET_ID_DETALLE_CONTROL_EFICIENCIA = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaControlEficienciaPorId";
        const string SP_GET_ID_DETALLE_IRREGULARIDAD = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaIrregularidadPorId";

        const string SP_INSERT = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaInsert";
        const string SP_MERGE_DETALLE = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaDetalleMerge";
        const string SP_MERGE_DETALLE_FOTO = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaFotosMerge";
        const string SP_MERGE_DETALLE_MAQUINA = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaMaquinaMerge";
        const string SP_MERGE_DETALLE_VACUNA = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaVacunaMerge";
        const string SP_MERGE_DETALLE_IRREGULARIDAD = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaIrregularidadMerge";
        const string SP_MERGE_DETALLE_CONTROL_EFICIENCIA = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaControlEficienciaMerge";

        const string SP_UPDATE = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaUpdate";
        const string SP_UPDATE_STATUS = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaStatusUpdate";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaDelete";

        public TxVacunacionSubCutaneaRepository(IConnectionSQL context)
            : base(context)
        {
        }

        public Task<IEnumerable<BE_TxVacunacionSubCutanea>> GetAll(FE_TxVacunacionSubCutanea entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutanea>(SP_GET, entidad));
        }

        public Task<BE_TxVacunacionSubCutanea> GetById(BE_TxVacunacionSubCutanea entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxVacunacionSubCutanea p = context.ExecuteSqlViewId<BE_TxVacunacionSubCutanea>(SP_GET_ID, entidad);
                if (p != null)
                {
                    p.ListarTxVacunacionSubCutaneaControlEficiencia = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaControlEficiencia>(SP_GET_ID_DETALLE, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaDetalle = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaDetalle>(SP_GET_ID_DETALLE_FOTOS, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaFotos = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaFotos>(SP_GET_ID_RESUMEN, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaIrregularidad = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaIrregularidad>(SP_GET_ID_RESUMEN, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaMaquina = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaMaquina>(SP_GET_ID_RESUMEN, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaVacuna = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaVacuna>(SP_GET_ID_RESUMEN, entidad).ToList();
                }
                else
                {
                    p = new BE_TxVacunacionSubCutanea();
                }
                return p;
            });
            return objListPrincipal;
        }

        public async Task<int> Create(BE_TxVacunacionSubCutanea value)
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

                                SqlParameter oParam = new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea);
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

                                cmd.Parameters.Add(new SqlParameter("@FlgAntibiotico", value.FlgAntibiotico));
                                cmd.Parameters.Add(new SqlParameter("@NombreAntibiotico", value.NombreAntibiotico));
                                cmd.Parameters.Add(new SqlParameter("@DosisAntibiotico", value.DosisAntibiotico));
                                cmd.Parameters.Add(new SqlParameter("@FlgPorcentajeViabilidad", value.FlgPorcentajeViabilidad));
                                cmd.Parameters.Add(new SqlParameter("@PuntajePorcentajeViabilidad", value.PuntajePorcentajeViabilidad));

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

                                value.IdVacunacionSubCutanea = (int)cmd.Parameters["@IdVacunacionSubCutanea"].Value;
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                            {
                                foreach (BE_TxVacunacionSubCutaneaDetalle item in value.ListarTxVacunacionSubCutaneaDetalle)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutaneaDetalle", item.IdVacunacionSubCutaneaDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea));
                                    cmd.Parameters.Add(new SqlParameter("@IdProcesoDetalleSubCutanea", item.IdProcesoDetalleSubCutanea));
                                    cmd.Parameters.Add(new SqlParameter("@Valor", item.Valor));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                            {
                                foreach (BE_TxVacunacionSubCutaneaControlEficiencia item in value.ListarTxVacunacionSubCutaneaControlEficiencia)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutaneaDetalle", item.IdVacunacionSubCutaneaDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea));
                                    cmd.Parameters.Add(new SqlParameter("@NombreVacunador", item.NombreVacunador));
                                    cmd.Parameters.Add(new SqlParameter("@CantidadInicial", item.CantidadInicial));
                                    cmd.Parameters.Add(new SqlParameter("@CantidadFinal", item.CantidadFinal));
                                    cmd.Parameters.Add(new SqlParameter("@VacunadoPorHora", item.VacunadoPorHora));
                                    cmd.Parameters.Add(new SqlParameter("@PuntajeProductividad", item.PuntajeProductividad));
                                    cmd.Parameters.Add(new SqlParameter("@Controlados", item.Controlados));
                                    cmd.Parameters.Add(new SqlParameter("@SinVacunar", item.SinVacunar));
                                    cmd.Parameters.Add(new SqlParameter("@Heridos", item.Heridos));
                                    cmd.Parameters.Add(new SqlParameter("@Mojados", item.Mojados));
                                    cmd.Parameters.Add(new SqlParameter("@MalaPosicion", item.MalaPosicion));
                                    cmd.Parameters.Add(new SqlParameter("@VacunadoCorrectos", item.VacunadoCorrectos));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                            {
                                foreach (BE_TxVacunacionSubCutaneaIrregularidad item in value.ListarTxVacunacionSubCutaneaIrregularidad)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutaneaDetalle", item.IdVacunacionSubCutaneaDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea));
                                    cmd.Parameters.Add(new SqlParameter("@NombreVacunador", item.NombreVacunador));
                                    cmd.Parameters.Add(new SqlParameter("@CodigoEquipo", item.CodigoEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@IdIrregularidad", item.IdIrregularidad));
                                    cmd.Parameters.Add(new SqlParameter("@Valor", item.Valor));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                            {
                                foreach (BE_TxVacunacionSubCutaneaMaquina item in value.ListarTxVacunacionSubCutaneaMaquina)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutaneaMaquina", item.IdVacunacionSubCutaneaMaquina));
                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea));
                                    cmd.Parameters.Add(new SqlParameter("@IdAguja", item.IdAguja));
                                    cmd.Parameters.Add(new SqlParameter("@NroMaquinas", item.NroMaquinas));
                                    cmd.Parameters.Add(new SqlParameter("@CodigoModelo", item.CodigoModelo));
                                    cmd.Parameters.Add(new SqlParameter("@CodigoEquipo", item.CodigoEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE, conn))
                            {
                                foreach (BE_TxVacunacionSubCutaneaVacuna item in value.ListarTxVacunacionSubCutaneaVacuna)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutaneaVacuna", item.IdVacunacionSubCutaneaVacuna));
                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea));
                                    cmd.Parameters.Add(new SqlParameter("@IdVacuna", item.IdVacuna));
                                    cmd.Parameters.Add(new SqlParameter("@NombreVacuna", item.NombreVacuna));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            if (value.ListarTxVacunacionSubCutaneaFotos.Count() > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_FOTO, conn))
                                {
                                    foreach (BE_TxVacunacionSubCutaneaFotos item in value.ListarTxVacunacionSubCutaneaFotos)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutaneaDetalle", item.IdVacunacionSubCutaneaDetalle));
                                        cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea));
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
                            value.IdVacunacionSubCutanea = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdVacunacionSubCutanea = 0;
            }

            return int.Parse(value.IdVacunacionSubCutanea.ToString());
        }

        public async Task Update(BE_TxVacunacionSubCutanea value)
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
                            if (value.ListarTxVacunacionSubCutaneaFotos.Count() > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_FOTO, conn))
                                {
                                    foreach (BE_TxVacunacionSubCutaneaFotos item in value.ListarTxVacunacionSubCutaneaFotos)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutaneaDetalle", item.IdVacunacionSubCutaneaDetalle));
                                        cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", item.IdVacunacionSubCutanea));
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
                            value.IdVacunacionSubCutanea = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdVacunacionSubCutanea = 0;
            }
        }
        public async Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxVacunacionSubCutanea entidad)
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
                var memoryPDF = await GenerarPDF(new BE_TxVacunacionSubCutanea { IdVacunacionSubCutanea = entidad.IdVacunacionSubCutanea });
                memory = memoryPDF;
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                return vResultadoTransaccion;
            }


            var data = context.ExecuteSqlViewId<BE_TxVacunacionSubCutanea>(SP_GET_ID_GOOGLE_DRIVE, new BE_TxVacunacionSubCutanea { IdVacunacionSubCutanea = entidad.IdVacunacionSubCutanea });
            var nameFile = string.Format("{0}.{1}", data.NombreArchivo, "pdf");

            try
            {
                EmailSenderRepository emailSenderRepository = new EmailSenderRepository(context);
                var mensaje = string.Format("Se envía informe de Vacunación SubCutanea - N° {0}", entidad.IdVacunacionSubCutanea);
                await emailSenderRepository.SendEmailAsync(data.EmailTo, "Correo Automatico - Vacunación SubCutanea", mensaje, new BE_MemoryStream { FileMemoryStream = memory }, nameFile);
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
                    //DescripcionEmpresa = data.DescripcionEmpresa,
                    //CodigoPlanta = data.CodigoPlanta,
                    //DescripcionPlanta = data.DescripcionPlanta,
                    //DescripcionTipoExplotacion = data.DescripcionTipoExplotacion,
                    //DescripcionSubTipoExplotacion = data.DescripcionSubTipoExplotacion,
                    //IdSubTipoExplotacion = data.IdSubTipoExplotacion,
                    IdDocumento = 0,
                    IdDocumentoReferencial = (int)data.IdVacunacionSubCutanea,
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
        public Task Delete(BE_TxVacunacionSubCutanea entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
        public async Task<MemoryStream> GenerarPDF(BE_TxVacunacionSubCutanea entidad)
        {
            BE_TxVacunacionSubCutanea item = await GetById(entidad);

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
