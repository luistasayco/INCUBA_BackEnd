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
    public class TxSIMRepository : RepositoryBase<BE_TxSIM>, ITxSIMRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_POR_FILTRO = DB_ESQUEMA + "INC_GetTxSIMPorFiltros";
        const string SP_GET_POR_CONSOLIDADO = DB_ESQUEMA + "INC_GetTxSIMPorCodigoEmpresa";
        const string SP_GET_POR_ID = DB_ESQUEMA + "INC_GetTxSIMPorId";
        const string SP_GET_POR_ID_DIGESTIVO = DB_ESQUEMA + "INC_GetTxSIMDigestivoPorId";
        const string SP_GET_POR_ID_FOTO = DB_ESQUEMA + "INC_GetTxSIMFotosPorId";
        const string SP_GET_POR_ID_INDICEBURSAL = DB_ESQUEMA + "INC_GetTxSIMIndiceBursalPorId";
        const string SP_GET_POR_ID_LESIONBURSAL = DB_ESQUEMA + "INC_GetTxSIMLesionBursaPorId";
        const string SP_GET_POR_ID_LESIONES = DB_ESQUEMA + "INC_GetTxSIMLesionesPorId";
        const string SP_GET_POR_ID_LESIONTIMO = DB_ESQUEMA + "INC_GetTxSIMLesionTimoPorId";
        const string SP_GET_POR_ID_RESPIRATORIO = DB_ESQUEMA + "INC_GetTxSIMRespiratorioPorId";

        const string SP_GET_ID_GOOGLE_DRIVE = DB_ESQUEMA + "INC_GetTxSIMPorIdGoogleDrive";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetTxSIMUpdate";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetTxSIMInsert";
        const string SP_UPDATE_STATUS = DB_ESQUEMA + "INC_SetTxSIMUpdateStatus";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetTxSIMDelete";

        const string SP_MERGE_DIGESTIVO = DB_ESQUEMA + "INC_SetTxSIMDigestivoMerge";
        const string SP_MERGE_FOTO = DB_ESQUEMA + "INC_SetTxSIMFotosMerge";
        const string SP_MERGE_INDICEBURSAL = DB_ESQUEMA + "INC_SetTxSIMIndiceBursalMerge";
        const string SP_MERGE_LESIONBURSAL = DB_ESQUEMA + "INC_SetTxSIMLesionBursaMerge";
        const string SP_MERGE_LESIONES = DB_ESQUEMA + "INC_SetTxSIMLesionesMerge";
        const string SP_MERGE_LESIONTIMO = DB_ESQUEMA + "INC_SetTxSIMLesionTimoMerge";
        const string SP_MERGE_RESPIRATORIO = DB_ESQUEMA + "INC_SetTxSIMRespiratorioMerge";


        public TxSIMRepository(IConnectionSQL context)
            : base(context)
        {
        }

        public Task<IEnumerable<BE_TxSIM>> GetAll(FE_TxSIM entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxSIM>(SP_GET_POR_FILTRO, entidad));
        }

        public Task<IEnumerable<BE_TxSIM>> GetByCodigoEmpresa(string codigoEmpresa)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxSIM>(SP_GET_POR_CONSOLIDADO, new FE_TxSIMConsolidadoCodigo { CodigoEmpresa = codigoEmpresa }));
        }

        public Task<BE_TxSIM> GetById(BE_TxSIM entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxSIM p = context.ExecuteSqlViewId<BE_TxSIM>(SP_GET_POR_ID, entidad);
                if (p != null)
                {
                    p.ListaTxSIMDigestivos = context.ExecuteSqlViewFindByCondition<BE_TxSIMDigestivo>(SP_GET_POR_ID_DIGESTIVO, entidad).ToList();
                    p.ListaTxSIMFotos = context.ExecuteSqlViewFindByCondition<BE_TxSIMFotos>(SP_GET_POR_ID_FOTO, entidad).ToList();
                    p.ListaTxSIMIndiceBursal = context.ExecuteSqlViewFindByCondition<BE_TxSIMIndiceBursal>(SP_GET_POR_ID_INDICEBURSAL, entidad).ToList();
                    p.ListaTxSIMLesionBursa = context.ExecuteSqlViewFindByCondition<BE_TxSIMLesionBursa>(SP_GET_POR_ID_LESIONBURSAL, entidad).ToList();
                    p.ListaTxSIMLesiones = context.ExecuteSqlViewFindByCondition<BE_TxSIMLesiones>(SP_GET_POR_ID_LESIONES, entidad).ToList();
                    p.ListaTxSIMLesionTimo = context.ExecuteSqlViewFindByCondition<BE_TxSIMLesionTimo>(SP_GET_POR_ID_LESIONTIMO, entidad).ToList();
                    p.ListaTxSIMRespiratorio = context.ExecuteSqlViewFindByCondition<BE_TxSIMRespiratorio>(SP_GET_POR_ID_RESPIRATORIO, entidad).ToList();
                }
                else
                {
                    p = new BE_TxSIM();
                }
                return p;
            });
            return objListPrincipal;
        }

        private BE_TxSIM GetByIdPDF(BE_TxSIM entidad)
        {
            BE_TxSIM p = context.ExecuteSqlViewId<BE_TxSIM>(SP_GET_POR_ID, entidad);
            p.ListaTxSIMDigestivos = context.ExecuteSqlViewFindByCondition<BE_TxSIMDigestivo>(SP_GET_POR_ID_DIGESTIVO, entidad).ToList();
            p.ListaTxSIMFotos = context.ExecuteSqlViewFindByCondition<BE_TxSIMFotos>(SP_GET_POR_ID_FOTO, entidad).ToList();
            p.ListaTxSIMIndiceBursal = context.ExecuteSqlViewFindByCondition<BE_TxSIMIndiceBursal>(SP_GET_POR_ID_INDICEBURSAL, entidad).ToList();
            p.ListaTxSIMLesionBursa = context.ExecuteSqlViewFindByCondition<BE_TxSIMLesionBursa>(SP_GET_POR_ID_LESIONBURSAL, entidad).ToList();
            p.ListaTxSIMLesiones = context.ExecuteSqlViewFindByCondition<BE_TxSIMLesiones>(SP_GET_POR_ID_LESIONES, entidad).ToList();
            p.ListaTxSIMLesionTimo = context.ExecuteSqlViewFindByCondition<BE_TxSIMLesionTimo>(SP_GET_POR_ID_LESIONTIMO, entidad).ToList();
            p.ListaTxSIMRespiratorio = context.ExecuteSqlViewFindByCondition<BE_TxSIMRespiratorio>(SP_GET_POR_ID_RESPIRATORIO, entidad).ToList();
            return p;
        }

        public async Task<int> Create(BE_TxSIM value)
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

                                SqlParameter oParam = new SqlParameter("@IdSIM", value.IdSIM);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@CodigoEmpresa", value.CodigoEmpresa));
                                cmd.Parameters.Add(new SqlParameter("@CodigoPlanta", value.CodigoPlanta));
                                cmd.Parameters.Add(new SqlParameter("@Edad", value.Edad));
                                cmd.Parameters.Add(new SqlParameter("@Sexo", value.Sexo));
                                cmd.Parameters.Add(new SqlParameter("@Zona", value.Zona));
                                cmd.Parameters.Add(new SqlParameter("@Galpon", value.Galpon));
                                cmd.Parameters.Add(new SqlParameter("@NroPollos", value.NroPollos));
                                cmd.Parameters.Add(new SqlParameter("@FecRegistro", value.FecRegistro));
                                cmd.Parameters.Add(new SqlParameter("@ResponsableInvetsa", value.ResponsableInvetsa));
                                cmd.Parameters.Add(new SqlParameter("@ResponsableIncubadora", value.ResponsableIncubadora));
                                cmd.Parameters.Add(new SqlParameter("@RelacionAFavorBursa", value.RelacionAFavorBursa));
                                cmd.Parameters.Add(new SqlParameter("@RelacionAFavorBazo", value.RelacionAFavorBazo));
                                cmd.Parameters.Add(new SqlParameter("@Relacion1a1", value.Relacion1a1));
                                cmd.Parameters.Add(new SqlParameter("@AparienciaNormal", value.AparienciaNormal));
                                cmd.Parameters.Add(new SqlParameter("@AparienciaAnormal", value.AparienciaAnormal));
                                cmd.Parameters.Add(new SqlParameter("@TamanoNormal", value.TamanoNormal));
                                cmd.Parameters.Add(new SqlParameter("@TamanoAnormal", value.TamanoAnormal));
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

                                value.IdSIM = (int)cmd.Parameters["@IdSIM"].Value;
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DIGESTIVO, conn))
                            {
                                foreach (BE_TxSIMDigestivo item in value.ListaTxSIMDigestivos)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdSIMDigestivo", item.IdSIMDigestivo));
                                    cmd.Parameters.Add(new SqlParameter("@IdSIM", value.IdSIM));
                                    cmd.Parameters.Add(new SqlParameter("@Ave", item.Ave));
                                    cmd.Parameters.Add(new SqlParameter("@Duademo", item.Duademo));
                                    cmd.Parameters.Add(new SqlParameter("@Yeyuno", item.Yeyuno));
                                    cmd.Parameters.Add(new SqlParameter("@Lleon", item.Lleon));
                                    cmd.Parameters.Add(new SqlParameter("@Ciegos", item.Ciegos));
                                    cmd.Parameters.Add(new SqlParameter("@Tonsillas", item.Tonsillas));
                                    cmd.Parameters.Add(new SqlParameter("@Higados", item.Higados));
                                    cmd.Parameters.Add(new SqlParameter("@Molleja", item.Molleja));
                                    cmd.Parameters.Add(new SqlParameter("@Proventriculo", item.Proventriculo));
                                    cmd.Parameters.Add(new SqlParameter("@FlgGradoLesion", item.FlgGradoLesion));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                            if (value.ListaTxSIMFotos != null)
                            {
                                if (value.ListaTxSIMFotos.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_FOTO, conn))
                                    {
                                        foreach (BE_TxSIMFotos item in value.ListaTxSIMFotos)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdSIMFoto", item.IdSIMFoto));
                                            cmd.Parameters.Add(new SqlParameter("@IdSIM", value.IdSIM));
                                            cmd.Parameters.Add(new SqlParameter("@Foto", item.Foto));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }

                            if (value.ListaTxSIMIndiceBursal != null)
                            {
                                if (value.ListaTxSIMIndiceBursal.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_INDICEBURSAL, conn))
                                    {
                                        foreach (BE_TxSIMIndiceBursal item in value.ListaTxSIMIndiceBursal)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdSIMIndiceBursal", item.IdSIMIndiceBursal));
                                            cmd.Parameters.Add(new SqlParameter("@IdSIM", value.IdSIM));
                                            cmd.Parameters.Add(new SqlParameter("@Ave", item.Ave));
                                            cmd.Parameters.Add(new SqlParameter("@PesoCorporal", item.PesoCorporal));
                                            cmd.Parameters.Add(new SqlParameter("@PesoBursa", item.PesoBursa));
                                            cmd.Parameters.Add(new SqlParameter("@PesoBazo", item.PesoBazo));
                                            cmd.Parameters.Add(new SqlParameter("@PesoTimo", item.PesoTimo));
                                            cmd.Parameters.Add(new SqlParameter("@PesoHigado", item.PesoHigado));
                                            cmd.Parameters.Add(new SqlParameter("@IndiceBursal", item.IndiceBursal));
                                            cmd.Parameters.Add(new SqlParameter("@IndiceTimico", item.IndiceTimico));
                                            cmd.Parameters.Add(new SqlParameter("@IndiceHepatico", item.IndiceHepatico));
                                            cmd.Parameters.Add(new SqlParameter("@Bursometro", item.Bursometro));
                                            cmd.Parameters.Add(new SqlParameter("@FlgPromedio", item.FlgPromedio));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }

                            if (value.ListaTxSIMLesionBursa != null)
                            {
                                if (value.ListaTxSIMLesionBursa.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_LESIONBURSAL, conn))
                                    {
                                        foreach (BE_TxSIMLesionBursa item in value.ListaTxSIMLesionBursa)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdSIMLesionesBursa", item.IdSIMLesionesBursa));
                                            cmd.Parameters.Add(new SqlParameter("@IdSIM", value.IdSIM));
                                            cmd.Parameters.Add(new SqlParameter("@Ave", item.Ave));
                                            cmd.Parameters.Add(new SqlParameter("@Valor", item.Valor));
                                            cmd.Parameters.Add(new SqlParameter("@FlgPromedio", item.FlgPromedio));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }
                            if (value.ListaTxSIMLesiones != null)
                            {
                                if (value.ListaTxSIMLesiones.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_LESIONES, conn))
                                    {
                                        foreach (BE_TxSIMLesiones item in value.ListaTxSIMLesiones)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdSIMLesiones", item.IdSIMLesiones));
                                            cmd.Parameters.Add(new SqlParameter("@IdSIM", value.IdSIM));
                                            cmd.Parameters.Add(new SqlParameter("@LesionesDeudemo", item.LesionesDeudemo));
                                            cmd.Parameters.Add(new SqlParameter("@LesionesIntestinoMedio", item.LesionesIntestinoMedio));
                                            cmd.Parameters.Add(new SqlParameter("@LesionesHigado", item.LesionesHigado));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }
                            if (value.ListaTxSIMLesionTimo != null)
                            {
                                if (value.ListaTxSIMLesionTimo.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_LESIONTIMO, conn))
                                    {
                                        foreach (BE_TxSIMLesionTimo item in value.ListaTxSIMLesionTimo)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdSIMLesionesTimo", item.IdSIMLesionesTimo));
                                            cmd.Parameters.Add(new SqlParameter("@IdSIM", value.IdSIM));
                                            cmd.Parameters.Add(new SqlParameter("@Ave", item.Ave));
                                            cmd.Parameters.Add(new SqlParameter("@Valor", item.Valor));
                                            cmd.Parameters.Add(new SqlParameter("@FlgPromedio", item.FlgPromedio));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }

                            if (value.ListaTxSIMRespiratorio != null)
                            {
                                if (value.ListaTxSIMRespiratorio.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_RESPIRATORIO, conn))
                                    {
                                        foreach (BE_TxSIMRespiratorio item in value.ListaTxSIMRespiratorio)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdSIMRespiratorio", item.IdSIMRespiratorio));
                                            cmd.Parameters.Add(new SqlParameter("@IdSIM", value.IdSIM));
                                            cmd.Parameters.Add(new SqlParameter("@Ave", item.Ave));
                                            cmd.Parameters.Add(new SqlParameter("@SacosAereos", item.SacosAereos));
                                            cmd.Parameters.Add(new SqlParameter("@Cornetes", item.Cornetes));
                                            cmd.Parameters.Add(new SqlParameter("@Glotis", item.Glotis));
                                            cmd.Parameters.Add(new SqlParameter("@Traquea", item.Traquea));
                                            cmd.Parameters.Add(new SqlParameter("@Pulmones", item.Pulmones));
                                            cmd.Parameters.Add(new SqlParameter("@Rinones", item.Rinones));
                                            cmd.Parameters.Add(new SqlParameter("@PlacaPeyer", item.PlacaPeyer));
                                            cmd.Parameters.Add(new SqlParameter("@FlgGradoLesion", item.FlgGradoLesion));
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
                            value.IdSIM = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdSIM = 0;
            }

            return int.Parse(value.IdSIM.ToString());
        }

        public async Task Update(BE_TxSIM value)
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

                                cmd.Parameters.Add(new SqlParameter("@IdSIM", value.IdSIM));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();
                            }

                            if (value.ListaTxSIMFotos.Count() > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_MERGE_FOTO, conn))
                                {
                                    foreach (BE_TxSIMFotos item in value.ListaTxSIMFotos)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.Add(new SqlParameter("@IdSIMFoto", item.IdSIMFoto));
                                        cmd.Parameters.Add(new SqlParameter("@IdSIM", value.IdSIM));
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
                            value.IdSIM = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdSIM = 0;
            }
        }
        public async Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxSIM entidad)
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
                var memoryPDF = await GenerarPDF(new BE_TxSIM { IdSIM = entidad.IdSIM });
                memory = memoryPDF;
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                return vResultadoTransaccion;
            }



            //Obtiene informacion del examen fisicion del pollito bebe
            var data = context.ExecuteSqlViewId<BE_TxSIM>(SP_GET_ID_GOOGLE_DRIVE, new BE_TxSIM { IdSIM = entidad.IdSIM });
            var nameFile = string.Format("{0}.{1}", data.NombreArchivo, "pdf");

            try
            {
                EmailSenderRepository emailSenderRepository = new EmailSenderRepository(context);
                var mensaje = string.Format("Se envía informe del Sistema Integral de Monitoreo de Campo - N° {0}", entidad.IdSIM);
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
                    IdDocumentoReferencial = (int)data.IdSIM,
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
        public Task Delete(BE_TxSIM entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
        public async Task<MemoryStream> GenerarPDF(BE_TxSIM entidad)
        {
            BE_TxSIM item = GetByIdPDF(entidad);

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

                var title = string.Format("SISTEMA INTEGRADO DE MONITOREO - {0}", entidad.IdSIM, titulo);
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
                var c4 = new PdfPCell(new Phrase("Zona:", parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c5 = new PdfPCell(new Phrase(item.Zona, parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                c1.Phrase = new Phrase("Granja:", parrafoNegro);
                c2.Phrase = new Phrase(item.CodigoPlanta, parrafoNegro);
                c3.Phrase = new Phrase("", parrafoNegro);
                c4.Phrase = new Phrase("Galpón:", parrafoNegro);
                c5.Phrase = new Phrase(item.Galpon.ToString(), parrafoNegro);
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                c1.Phrase = new Phrase("Edad:", parrafoNegro);
                c2.Phrase = new Phrase(item.Edad.ToString(), parrafoNegro);
                c3.Phrase = new Phrase("", parrafoNegro);
                c4.Phrase = new Phrase("N° Pollos:", parrafoNegro);
                c5.Phrase = new Phrase(item.NroPollos.ToString(), parrafoNegro);
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                c1.Phrase = new Phrase("Sexo:", parrafoNegro);
                c2.Phrase = new Phrase(item.Sexo, parrafoNegro);
                c3.Phrase = new Phrase("", parrafoNegro);
                c4.Phrase = new Phrase("Fecha:", parrafoNegro);
                c5.Phrase = new Phrase(DateTime.Parse(item.FecHoraRegistro.ToString()).ToShortDateString(), parrafoNegro);
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                tbl.AddCell(c5);
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 5;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 12f, 11f, 11f, 11f, 11f, 11f, 11f, 11f, 11f }) { WidthPercentage = 100 };
                c1 = new PdfPCell();

                c1 = new PdfPCell(new Phrase("Peso Corporal (g)", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Peso de Bursa(g)", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Peso de Bazo(g)", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Peso de Timo(g)", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Peso de Hígado(g)", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Índice Bursal", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Índice Timico", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Índice Hepático", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Bursómetro", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);

                foreach (var detalle in item.ListaTxSIMIndiceBursal)
                {
                    c1 = new PdfPCell(new Phrase(((int)detalle.PesoCorporal).ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.PesoBursa.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.PesoBazo.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.PesoTimo.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.PesoHigado.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.IndiceBursal.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.IndiceTimico.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.IndiceHepatico.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Bursometro.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                }
                // Agamos una lina en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 9;
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);


                tbl = new PdfPTable(new float[] { 20f, 80f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Referencia:A18:", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Índice bursal: Peso de bursa / Peso corporal x 1000", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegrita);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Valores entre 0,5 - 1,0 son indicativos de atrofia bursal. Valores menores de 0,5 indican atrofia severa de la bursa. Estos índices son aplicables mayormente a pollos entre 21 y 28 días.", parrafoNegro);
                tbl.AddCell(c1);
                // Agamos una lina en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 2;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 17f, 5f, 7f, 17f, 5f, 7f, 17f, 5f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("1. Relación Morfométrica Bursa / Bazo: ", parrafoNegrita);
                c1.Colspan = 2;
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("2. Apariencia de los pliegues internos de la bursa: ", parrafoNegrita);
                c1.Colspan = 2;
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("3. Tamaño y apariencia del Timo: ", parrafoNegrita);
                c1.Colspan = 2;
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("A favor de la Bursa:", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(item.RelacionAFavorBursa.ToString(), parrafoNegrita);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Normal: ", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(item.AparienciaNormal.ToString(), parrafoNegrita);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Normal: ", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(item.TamanoNormal.ToString(), parrafoNegrita);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("A favor del Bazo:", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(item.RelacionAFavorBazo.ToString(), parrafoNegrita);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Anormal: ", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(item.AparienciaAnormal.ToString(), parrafoNegrita);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Anormal: ", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(item.TamanoAnormal.ToString(), parrafoNegrita);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Relación 1 / 1:", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(item.Relacion1a1.ToString(), parrafoNegrita);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                c1.Colspan = 6;
                tbl.AddCell(c1);

                // Agamos una lina en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 8;
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 16f, 12f, 12f, 12f, 12f, 12f, 12f, 12f }) { WidthPercentage = 100 };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("SISTEMA RESPIRATORIO", subTitulo);
                c1.Colspan = 8;
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("Ave", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Sacos aereos", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Cornetes", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Glotis", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Tráquea", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Pulmones", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Riñones", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Placa Peyer", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);

                foreach (var detalle in item.ListaTxSIMRespiratorio)
                {
                    if(detalle.FlgGradoLesion)
                    {
                        c1 = new PdfPCell(new Phrase("Grado de lesión", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                    }
                    else
                    {
                        c1 = new PdfPCell(new Phrase(((int)detalle.Ave).ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                    }
                    c1 = new PdfPCell(new Phrase(detalle.SacosAereos.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Cornetes.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Glotis.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Traquea.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Pulmones.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Rinones.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.PlacaPeyer.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                }
                // Agamos una lina en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 8;
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 16f, 10f, 11f, 10f, 10f, 10f, 10f, 10f, 12f }) { WidthPercentage = 100 };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("SISTEMA DIGESTIVO", subTitulo);
                c1.Colspan = 9;
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();

                c1 = new PdfPCell(new Phrase("Ave", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Duodeno", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Yeyuno", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Ileon", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Ciegos", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Tonsilas C.", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Hígado", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Molleja", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Proventriculo", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);

                foreach (var detalle in item.ListaTxSIMDigestivos)
                {
                    if (detalle.FlgGradoLesion)
                    {
                        c1 = new PdfPCell(new Phrase("Grado de lesión", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                    }
                    else
                    {
                        c1 = new PdfPCell(new Phrase(((int)detalle.Ave).ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                    }
                    c1 = new PdfPCell(new Phrase(detalle.Duademo.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Yeyuno.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Lleon.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Ciegos.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Tonsillas.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Higados.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Molleja.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell(new Phrase(detalle.Proventriculo.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                }
                // Agamos una lina en blanco
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 9;
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 15f , 15f, 70f}) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Interpretación", parrafoNegrita);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Lesión", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("0.2-1", parrafoNegrita);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Leve", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("1.2-2 ", parrafoNegrita);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Moderado", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Los grados de lesión serán calificados del 1 al 3 según su extensión en le órgano", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("2.2-3", parrafoNegrita);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Severo", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);

                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Colspan = 3;
                c1.Border = 0;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Hígado", parrafoNegrita);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Congestión", parrafoNegrita);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("1", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Pálido", parrafoNegrita);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("2", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Friable", parrafoNegrita);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("3", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);

                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                c1.Colspan = 3;
                tbl.AddCell(c1);
               
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                c1.Colspan = 3;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 6f, 41f, 6f, 41F, 6f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                tbl.AddCell(c1);

                var tbl1 = new PdfPTable(new float[] { 50f, 50F }) { WidthPercentage = 80f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("SCORE DE LESIONES HISTOPATOLÓGICAS DE BURSA", subTituloParticiones);
                c1.Colspan = 2;
                c1.Border = 0;
                tbl1.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Ave", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl1.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Valor", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl1.AddCell(c1);

                foreach (var detalle in item.ListaTxSIMLesionBursa)
                {
                    c1 = new PdfPCell();
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    if (detalle.FlgPromedio)
                    {
                        c1.Phrase = new Phrase("Promedio", parrafoNegro);
                        tbl1.AddCell(c1);
                    } else
                    {
                        c1.Phrase = new Phrase(detalle.Ave.ToString(), parrafoNegro);
                        tbl1.AddCell(c1);
                    }

                    c1.Phrase = new Phrase(detalle.Valor.ToString(), parrafoNegro);
                    tbl1.AddCell(c1);
                }
                c1 = new PdfPCell(tbl1);
                c1.Border = 0;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                tbl.AddCell(c1);

                var tbl2 = new PdfPTable(new float[] { 50f, 50F }) { WidthPercentage = 80f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("SCORE DE LESIONES HISTOPATOLÓGICAS DE TIMO", subTituloParticiones);
                c1.Colspan = 2;
                c1.Border = 0;
                tbl2.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Ave", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl2.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Valor", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl2.AddCell(c1);

                foreach (var detalle in item.ListaTxSIMLesionTimo)
                {
                    c1 = new PdfPCell();
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    if (detalle.FlgPromedio)
                    {
                        c1.Phrase = new Phrase("Promedio", parrafoNegro);
                        tbl2.AddCell(c1);
                    }
                    else
                    {
                        c1.Phrase = new Phrase(detalle.Ave.ToString(), parrafoNegro);
                        tbl2.AddCell(c1);
                    }

                    c1.Phrase = new Phrase(detalle.Valor.ToString(), parrafoNegro);
                    tbl2.AddCell(c1);
                }

                c1 = new PdfPCell(tbl2);
                c1.Border = 0;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                c1.Colspan = 5;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                c1.Colspan = 5;
                tbl.AddCell(c1);

                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 33f, 33f, 34f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("Lesiones en Duodeno", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Lesiones en Intestino Medio", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Lesiones en Hígado", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);

                foreach (var detalle in item.ListaTxSIMLesiones)
                {
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(detalle.LesionesDeudemo, parrafoNegro);
                    tbl.AddCell(c1);

                    c1.Phrase = new Phrase(detalle.LesionesIntestinoMedio, parrafoNegro);
                    tbl.AddCell(c1);

                    c1.Phrase = new Phrase(detalle.LesionesHigado, parrafoNegro);
                    tbl.AddCell(c1);
                }

                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                c1.Colspan = 3;
                tbl.AddCell(c1);

                c1.Phrase = new Phrase("", parrafoNegrita);
                c1.Border = 0;
                c1.Colspan = 3;
                tbl.AddCell(c1);

                doc.Add(tbl);


                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("FOTOS Y OBSERVACIONES", subTitulo);
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
                doc.Add(tbl);

                var countFotos = item.ListaTxSIMFotos.Count();
                iTextSharp.text.Image foto = null;

                if (countFotos > 0)
                {

                    doc.Add(new Phrase("Fotos", subTitulo));

                    doc.Add(new Phrase(" "));

                    tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f };
                    c1 = new PdfPCell();

                    foreach (var detalle7 in item.ListaTxSIMFotos)
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
