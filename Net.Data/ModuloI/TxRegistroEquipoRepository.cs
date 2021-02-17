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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Net.Data
{
    public class TxRegistroEquipoRepository : RepositoryBase<BE_TxRegistroEquipo>, ITxRegistroEquipoRepository
    {

        private string _aplicacionName;
        private string _metodoName;
        private readonly Regex regex = new Regex(@"<(\w+)>.*");

        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetTxRegistroEquipoPorFiltros";

        //Detalle New 
        const string SP_GET_PRINCIPAL = DB_ESQUEMA + "INC_GetRequerimientoEquipoNew";
        const string SP_GET_DETALLE_1 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle1New";
        const string SP_GET_DETALLE_2 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle2New";
        const string SP_GET_DETALLE_2_NO_PREDETERMINADO = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle2NoPredeterminadoNew";
        const string SP_GET_DETALLE_3 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle3New";
        const string SP_GET_DETALLE_4 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle4New";
        const string SP_GET_DETALLE_6 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle6New";


        const string SP_GET_ID = DB_ESQUEMA + "INC_GetTxRegistroEquipoPorId";
        const string SP_GET_ID_DETALLE_1 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle1PorId";
        const string SP_GET_ID_DETALLE_2 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle2PorId";
        const string SP_GET_ID_DETALLE_2_PDF = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle2PorIdPdf";
        const string SP_GET_ID_DETALLE_3 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle3PorId";
        const string SP_GET_ID_DETALLE_4 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle4PorId";
        const string SP_GET_ID_DETALLE_5 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle5PorId";
        const string SP_GET_ID_DETALLE_6Repuesto = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle6RepuestoPorId";
        const string SP_GET_ID_DETALLE_6 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle6PorId";
        const string SP_GET_ID_DETALLE_7 = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle7PorId";

        const string SP_INSERT = DB_ESQUEMA + "INC_SetTxRegistroEquipoInsert";
        const string SP_INSERT_DETALLE_1 = DB_ESQUEMA + "INC_SetxRegistroEquipoDetalle1Merge";
        const string SP_INSERT_DETALLE_2 = DB_ESQUEMA + "INC_SetxRegistroEquipoDetalle2Merge";
        const string SP_INSERT_DETALLE_3 = DB_ESQUEMA + "INC_SetxRegistroEquipoDetalle3Merge";
        const string SP_INSERT_DETALLE_4 = DB_ESQUEMA + "INC_SetxRegistroEquipoDetalle4Merge";
        const string SP_INSERT_DETALLE_5 = DB_ESQUEMA + "INC_SetxRegistroEquipoDetalle5Merge";
        const string SP_INSERT_DETALLE_6 = DB_ESQUEMA + "INC_SetxRegistroEquipoDetalle6Merge";
        const string SP_INSERT_DETALLE_7 = DB_ESQUEMA + "INC_SetxRegistroEquipoDetalle7Merge";

        const string SP_DELETE = DB_ESQUEMA + "INC_SetTxRegistroEquipoDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetTxRegistroEquipoUpdate";
        const string SP_UPDATE_STATUS = DB_ESQUEMA + "INC_SetTxRegistroEquipoStatusUpdate";
        const string SP_GET_ID_GOOGLE_DRIVE = DB_ESQUEMA + "INC_GetTxRegistroEquipoPorIdGoogleDrive";

        public TxRegistroEquipoRepository(IConnectionSQL context)
            : base(context)
        {
        }

        public Task<IEnumerable<BE_TxRegistroEquipo>> GetAll(BE_TxRegistroEquipo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_TxRegistroEquipo> GetNewObject(BE_General entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxRegistroEquipo p = new BE_TxRegistroEquipo();
                p.IdRegistroEquipo = 0;
                p.CodigoEmpresa = entidad.CodigoEmpresa;
                p.CodigoPlanta = entidad.CodigoPlanta;
                p.CodigoModelo = entidad.CodigoModelo;

                IEnumerable<BE_TxRegistroEquipoDetalle1> objListDetalle1 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle1>(SP_GET_DETALLE_1, entidad);

                IEnumerable<BE_TxRegistroEquipoDetalle2> objListDetalle2 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle2>(SP_GET_DETALLE_2, entidad);
                IEnumerable<BE_TxRegistroEquipoDetalle2> objListDetalle2_no_preterminado = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle2>(SP_GET_DETALLE_2_NO_PREDETERMINADO, entidad);

                IEnumerable<BE_TxRegistroEquipoDetalle3> objListDetalle3 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle3>(SP_GET_DETALLE_3, entidad);

                IEnumerable<BE_TxRegistroEquipoDetalle4> objListDetalle4 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle4>(SP_GET_DETALLE_4, entidad);

                IEnumerable<BE_TxRegistroEquipoDetalle6> objListDetalle6 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle6>(SP_GET_DETALLE_6, entidad);

                p.TxRegistroEquipoDetalle1 = objListDetalle1.ToList();
                p.TxRegistroEquipoDetalle2 = objListDetalle2.ToList();
                p.TxRegistroEquipoDetalle2NoPredeterminado = objListDetalle2_no_preterminado.ToList();
                p.TxRegistroEquipoDetalle3 = objListDetalle3.ToList();
                p.TxRegistroEquipoDetalle4 = objListDetalle4.ToList();
                p.TxRegistroEquipoDetalle6 = objListDetalle6.ToList();

                return p;
            });
            return objListPrincipal;
        }
        public Task<BE_TxRegistroEquipo> GetById(BE_TxRegistroEquipo entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxRegistroEquipo p = context.ExecuteSqlViewId<BE_TxRegistroEquipo>(SP_GET_ID, entidad);
                if (p != null)
                {
                    p.TxRegistroEquipoDetalle1 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle1>(SP_GET_ID_DETALLE_1, entidad).ToList();
                    p.TxRegistroEquipoDetalle2 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle2>(SP_GET_ID_DETALLE_2, entidad).ToList();
                    p.TxRegistroEquipoDetalle3 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle3>(SP_GET_ID_DETALLE_3, entidad).ToList();
                    p.TxRegistroEquipoDetalle4 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle4>(SP_GET_ID_DETALLE_4, entidad).ToList();
                    p.TxRegistroEquipoDetalle5 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle5>(SP_GET_ID_DETALLE_5, entidad).ToList();
                    p.TxRegistroEquipoDetalle6 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle6>(SP_GET_ID_DETALLE_6, entidad).ToList();
                    p.TxRegistroEquipoDetalle6Repuestos = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle6>(SP_GET_ID_DETALLE_6Repuesto, entidad).ToList();
                    p.TxRegistroEquipoDetalle7 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle7>(SP_GET_ID_DETALLE_7, entidad).ToList();
                } else
                {
                    p = new BE_TxRegistroEquipo();
                }
                return p;
            });
            return objListPrincipal;
        }
        private BE_TxRegistroEquipo GetByIdPDF(BE_TxRegistroEquipo entidad)
        {
            BE_TxRegistroEquipo p = context.ExecuteSqlViewId<BE_TxRegistroEquipo>(SP_GET_ID, entidad);
            if (p != null)
            {
                p.TxRegistroEquipoDetalle1 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle1>(SP_GET_ID_DETALLE_1, entidad).ToList();
                p.TxRegistroEquipoDetalle2 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle2>(SP_GET_ID_DETALLE_2_PDF, entidad).ToList();
                p.TxRegistroEquipoDetalle3 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle3>(SP_GET_ID_DETALLE_3, entidad).ToList();
                p.TxRegistroEquipoDetalle4 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle4>(SP_GET_ID_DETALLE_4, entidad).ToList();
                p.TxRegistroEquipoDetalle5 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle5>(SP_GET_ID_DETALLE_5, entidad).ToList();
                p.TxRegistroEquipoDetalle6 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle6>(SP_GET_ID_DETALLE_6, entidad).ToList();
                p.TxRegistroEquipoDetalle6Repuestos = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle6>(SP_GET_ID_DETALLE_6Repuesto, entidad).ToList();
                p.TxRegistroEquipoDetalle7 = context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle7>(SP_GET_ID_DETALLE_7, entidad).ToList();
            }
            else
            {
                p = new BE_TxRegistroEquipo();
            }
            return p;
        }
        public async Task<BE_ResultadoTransaccion> Create(BE_TxRegistroEquipo value)
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

                                SqlParameter oParam = new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@CodigoEmpresa", value.CodigoEmpresa));
                                cmd.Parameters.Add(new SqlParameter("@CodigoPlanta", value.CodigoPlanta));
                                cmd.Parameters.Add(new SqlParameter("@CodigoModelo", value.CodigoModelo));
                                cmd.Parameters.Add(new SqlParameter("@FecRegistro", value.FecRegistro));
                                cmd.Parameters.Add(new SqlParameter("@FirmaIncuba", value.FirmaIncuba));
                                cmd.Parameters.Add(new SqlParameter("@FirmaPlanta", value.FirmaPlanta));
                                cmd.Parameters.Add(new SqlParameter("@IdUsuarioCierre", value.IdUsuarioCierre));
                                cmd.Parameters.Add(new SqlParameter("@FecCierre", value.FecCierre));
                                cmd.Parameters.Add(new SqlParameter("@FlgCerrado", value.FlgCerrado));
                                cmd.Parameters.Add(new SqlParameter("@ResponsableIncuba", value.ResponsableIncuba));
                                cmd.Parameters.Add(new SqlParameter("@ResponsablePlanta", value.ResponsablePlanta));
                                cmd.Parameters.Add(new SqlParameter("@EmailFrom", value.EmailFrom));
                                cmd.Parameters.Add(new SqlParameter("@EmailTo", value.EmailTo));
                                cmd.Parameters.Add(new SqlParameter("@JefePlanta", value.JefePlanta));
                                cmd.Parameters.Add(new SqlParameter("@ObservacionesInvetsa", value.ObservacionesInvetsa));
                                cmd.Parameters.Add(new SqlParameter("@ObservacionesPlanta", value.ObservacionesPlanta));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();

                                value.IdRegistroEquipo = (int)cmd.Parameters["@IdRegistroEquipo"].Value;
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_INSERT_DETALLE_1, conn))
                            {
                                foreach (BE_TxRegistroEquipoDetalle1 item in value.TxRegistroEquipoDetalle1)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipoDetalle", item.IdRegistroEquipoDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@IdMantenimientoPorModelo", item.IdMantenimientoPorModelo));
                                    cmd.Parameters.Add(new SqlParameter("@CodigoEquipo", item.CodigoEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@FlgValor", item.FlgValor));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_INSERT_DETALLE_2, conn))
                            {
                                foreach (BE_TxRegistroEquipoDetalle2 item in value.TxRegistroEquipoDetalle2)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipoDetalle", item.IdRegistroEquipoDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@IdRepuestoPorModelo", item.IdRepuestoPorModelo));
                                    cmd.Parameters.Add(new SqlParameter("@CodigoEquipo", item.CodigoEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@MP", item.Mp));
                                    cmd.Parameters.Add(new SqlParameter("@FlgValor", item.FlgValor));
                                    cmd.Parameters.Add(new SqlParameter("@RFC", item.Rfc));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_INSERT_DETALLE_3, conn))
                            {
                                foreach (BE_TxRegistroEquipoDetalle3 item in value.TxRegistroEquipoDetalle3)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipoDetalle", item.IdRegistroEquipoDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@IdCondicionLimpieza", item.IdCondicionLimpieza));
                                    cmd.Parameters.Add(new SqlParameter("@FlgValor", item.FlgValor));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_INSERT_DETALLE_4, conn))
                            {
                                foreach (BE_TxRegistroEquipoDetalle4 item in value.TxRegistroEquipoDetalle4)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipoDetalle", item.IdRegistroEquipoDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@IdRequerimientoEquipo", item.IdRequerimientoEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@FlgValor", item.FlgValor));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            if (value.TxRegistroEquipoDetalle5 != null)
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_INSERT_DETALLE_5, conn))
                                {

                                    foreach (BE_TxRegistroEquipoDetalle5 item in value.TxRegistroEquipoDetalle5)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipoDetalle", item.IdRegistroEquipoDetalle));
                                        cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo));
                                        cmd.Parameters.Add(new SqlParameter("@IdRepuestoPorModelo", item.IdRepuestoPorModelo));
                                        cmd.Parameters.Add(new SqlParameter("@CodigoEquipo", item.CodigoEquipo));
                                        cmd.Parameters.Add(new SqlParameter("@Observacion", item.Observacion));
                                        cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                        cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_INSERT_DETALLE_6, conn))
                            {
                                foreach (BE_TxRegistroEquipoDetalle6 item in value.TxRegistroEquipoDetalle6)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipoDetalle", item.IdRegistroEquipoDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@CodigoRepuesto", item.CodigoRepuesto));
                                    cmd.Parameters.Add(new SqlParameter("@StockActual", item.StockActual));
                                    cmd.Parameters.Add(new SqlParameter("@CambioPorMantenimiento", item.CambioPorMantenimiento));
                                    cmd.Parameters.Add(new SqlParameter("@Entregado", item.Entregado));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            if (value.TxRegistroEquipoDetalle6Repuestos != null)
                            {
                                using (SqlCommand cmd = new SqlCommand(SP_INSERT_DETALLE_6, conn))
                                {
                                    foreach (BE_TxRegistroEquipoDetalle6 item in value.TxRegistroEquipoDetalle6Repuestos)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipoDetalle", item.IdRegistroEquipoDetalle));
                                        cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo));
                                        cmd.Parameters.Add(new SqlParameter("@CodigoRepuesto", item.CodigoRepuesto));
                                        cmd.Parameters.Add(new SqlParameter("@StockActual", item.StockActual));
                                        cmd.Parameters.Add(new SqlParameter("@CambioPorMantenimiento", item.CambioPorMantenimiento));
                                        cmd.Parameters.Add(new SqlParameter("@Entregado", item.Entregado));
                                        cmd.Parameters.Add(new SqlParameter("@Unchecked", 1));
                                        cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                        cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                }
                            }
                            if (value.TxRegistroEquipoDetalle7 != null)
                            {
                                if (value.TxRegistroEquipoDetalle7.Count > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_INSERT_DETALLE_7, conn))
                                    {
                                        foreach (BE_TxRegistroEquipoDetalle7 item in value.TxRegistroEquipoDetalle7)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipoDetalle", item.IdRegistroEquipoDetalle));
                                            cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo));
                                            cmd.Parameters.Add(new SqlParameter("@Foto", item.Foto));
                                            cmd.Parameters.Add(new SqlParameter("@Orden", item.Orden));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }

                            transaction.Commit();

                            vResultadoTransaccion.IdRegistro = (int)value.IdRegistroEquipo;
                            vResultadoTransaccion.ResultadoCodigo = 0;
                            vResultadoTransaccion.ResultadoDescripcion = "Se genero correctamente...!!!";

                        }
                        catch (Exception ex)
                        {
                            vResultadoTransaccion.IdRegistro = -1;
                            vResultadoTransaccion.ResultadoCodigo = -1;
                            vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                            transaction.Rollback();
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
        public async Task Update(BE_TxRegistroEquipo value)
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

                            cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo));
                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                            await cmd.ExecuteNonQueryAsync();
                        }

                        if (value.TxRegistroEquipoDetalle7 != null)
                        {
                            using (SqlCommand cmd = new SqlCommand(SP_INSERT_DETALLE_7, conn))
                            {
                                foreach (BE_TxRegistroEquipoDetalle7 item in value.TxRegistroEquipoDetalle7)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipoDetalle", item.IdRegistroEquipoDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo));
                                    cmd.Parameters.Add(new SqlParameter("@Foto", item.Foto));
                                    cmd.Parameters.Add(new SqlParameter("@Orden", item.Orden));
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
                        transaction.Rollback();
                    }
                }
            }
        }
        public async Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxRegistroEquipo entidad)
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

            //Obtiene informacion del examen fisicion del pollito bebe
            var data = context.ExecuteSqlViewId<BE_TxRegistroEquipo>(SP_GET_ID_GOOGLE_DRIVE, new BE_TxRegistroEquipo { IdRegistroEquipo = entidad.IdRegistroEquipo });
            var nameFile = string.Format("{0}.{1}", data.NombreArchivo, "pdf");
            var memory = new MemoryStream();
            try
            {
                var memoryPDF = await GenerarPDF(new BE_TxRegistroEquipo { IdRegistroEquipo = data.IdRegistroEquipo });
                memory = memoryPDF;
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                return vResultadoTransaccion;
            }

            try
            {
                EmailSenderRepository emailSenderRepository = new EmailSenderRepository(context);
                var mensaje = string.Format("Se envía informe de Inspección de Equipo - N° {0}", data.IdRegistroEquipo);
                await emailSenderRepository.SendEmailAsync(data.EmailTo, "Correo Automatico - Inspección de Equipo", mensaje, new BE_MemoryStream { FileMemoryStream = memory }, nameFile);
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
                    IdDocumentoReferencial = (int)data.IdRegistroEquipo,
                    FlgCerrado = true,
                    IdUsuarioCierre = entidad.RegUsuario,
                    FecCerrado = DateTime.Now,
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
        public Task Delete(BE_TxRegistroEquipo entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
        public async Task<MemoryStream> GenerarPDF(BE_TxRegistroEquipo entidad)
        {
            BE_TxRegistroEquipo item = GetByIdPDF(entidad);

            var fontWebdings = "resources/font/webdings.ttf";

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
                BaseFont webdings = BaseFont.CreateFont(fontWebdings, BaseFont.CP1250, true);
                // Titulo
                iTextSharp.text.Font titulo = new iTextSharp.text.Font(helvetica, 20f, iTextSharp.text.Font.BOLD, new BaseColor(103, 93, 152));
                iTextSharp.text.Font subTitulo = new iTextSharp.text.Font(helvetica, 14f, iTextSharp.text.Font.BOLD, new BaseColor(103, 93, 152));
                iTextSharp.text.Font parrafoBlanco = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, BaseColor.White);
                iTextSharp.text.Font parrafoNegro = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font parrafoNegroWebdings = new iTextSharp.text.Font(webdings, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font parrafoNegrita = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, BaseColor.Black);
                pe.HeaderLeft = " ";
                pe.HeaderFont = parrafoBlanco;
                pe.HeaderRight = " ";
                doc.Open();

                var tblTitulo = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };
                var title = string.Format("INFORME DE INSPECCIÓN DE EQUIPOS - {0}", entidad.IdRegistroEquipo, titulo);
                var c1Titulo = new PdfPCell(new Phrase(title, titulo)) { Border = 0 };
                c1Titulo.HorizontalAlignment = Element.ALIGN_CENTER;
                c1Titulo.VerticalAlignment = Element.ALIGN_MIDDLE;
                tblTitulo.AddCell(c1Titulo);
                doc.Add(tblTitulo);

                doc.Add(new Phrase(" "));
                doc.Add(new Phrase(" "));

                var tbl = new PdfPTable(new float[] { 25f, 43f, 12f, 20f }) { WidthPercentage = 100 };
                var c1 = new PdfPCell(new Phrase("Compañia", parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c2 = new PdfPCell(new Phrase(item.DescripcionEmpresa, parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c3 = new PdfPCell(new Phrase("Fecha/Hora", parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                var c4 = new PdfPCell(new Phrase(item.FecHoraRegistro.ToString(), parrafoNegro)) { Border = 0, PaddingBottom = 5f };
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);

                c1.Phrase = new Phrase("Planta de Incubación", parrafoNegro);
                c2.Phrase = new Phrase(item.DescripcionPlanta, parrafoNegro);
                c3.Phrase = new Phrase("Modelo", parrafoNegro);
                c4.Phrase = new Phrase(item.DescripcionModelo, parrafoNegro);
                tbl.AddCell(c1);
                tbl.AddCell(c2);
                tbl.AddCell(c3);
                tbl.AddCell(c4);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 25f, 75f }) { WidthPercentage = 100 };
                //c1.Phrase = new Phrase("Dirección", parrafoNegro);
                //c2.Phrase = new Phrase(" ", parrafoNegro);
                //tbl.AddCell(c1);
                //tbl.AddCell(c2);

                c1.Phrase = new Phrase("Jefe de Planta", parrafoNegro);
                c2.Phrase = new Phrase(item.JefePlanta, parrafoNegro);
                tbl.AddCell(c1);
                tbl.AddCell(c2);

                c1.Phrase = new Phrase("Encargado de Planta", parrafoNegro);
                c2.Phrase = new Phrase(item.ResponsablePlanta, parrafoNegro);
                tbl.AddCell(c1);
                tbl.AddCell(c2);

                doc.Add(tbl);

                doc.Add(new Phrase("\n"));

                doc.Add(new Phrase("A. Detalles de Mantenimiento y Funcionamiento de equipos", subTitulo));

                doc.Add(new Phrase(" "));


                // Obtenemos un Item Para saber cuantas maquinas existe
                var itemUnico = item.TxRegistroEquipoDetalle1.FirstOrDefault();


                // Contamos la cantidad de item que tiene
                int CountItemHeader = item.TxRegistroEquipoDetalle1.FindAll(x => x.Descripcion == itemUnico.Descripcion).Count;

                var listDetalle1Header = item.TxRegistroEquipoDetalle1.FindAll(x => x.Descripcion == itemUnico.Descripcion);

                tbl = new PdfPTable(CountItemHeader + 7) { WidthPercentage = 100 };
                PdfPCell CellMaster = new PdfPCell();

                int xFila = 1;

                foreach (var detalle1 in listDetalle1Header)
                {
                    if (xFila == 1)
                    {
                        CellMaster = new PdfPCell(new Phrase("Item", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        CellMaster.Rotation = 90;
                        tbl.AddCell(CellMaster);
                        CellMaster = new PdfPCell(new Phrase("Descripción", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        CellMaster.Colspan = 6;
                        tbl.AddCell(CellMaster);
                    }

                    CellMaster = new PdfPCell(new Phrase(detalle1.CodigoEquipo, parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    CellMaster.Rotation = 90;
                    tbl.AddCell(CellMaster);

                    xFila = xFila + 1;
                }

                xFila = 1;
                var xSecuencia = 1;
                foreach (var detalle1 in item.TxRegistroEquipoDetalle1)
                {
                    
                    if (xFila == 1)
                    {
                        CellMaster = new PdfPCell(new Phrase(xSecuencia.ToString(), parrafoNegro)) {  HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(CellMaster);
                        CellMaster = new PdfPCell(new Phrase(detalle1.Descripcion, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                        CellMaster.Colspan = 6;
                        CellMaster.FixedHeight = 25f;
                        tbl.AddCell(CellMaster);
                        xSecuencia = xSecuencia + 1;
                    }

                    CellMaster = new PdfPCell(new Phrase(detalle1.FlgValor ? "a" : "", parrafoNegroWebdings)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    if (!detalle1.FlgValor)
                    {
                        CellMaster.BackgroundColor = BaseColor.Red;
                    }
                    

                    CellMaster.Rotation = 0;
                    tbl.AddCell(CellMaster);

                    if (xFila == CountItemHeader)
                    {
                        xFila = 0;
                    }

                    xFila = xFila + 1;
                }

                doc.Add(tbl);

                doc.Add(new Phrase("\n"));

                doc.Add(new Phrase("B. Check list de componentes", subTitulo));

                doc.Add(new Phrase(" "));

                tbl = new PdfPTable(CountItemHeader + 11) { WidthPercentage = 100 };
                CellMaster = new PdfPCell();

                xFila = 1;

                var itemRepuesto = item.TxRegistroEquipoDetalle2.FirstOrDefault().CodigoRepuesto;

                List<BE_TxRegistroEquipoDetalle2> listHeader2 = item.TxRegistroEquipoDetalle2.FindAll(x => x.CodigoRepuesto == itemRepuesto);

                foreach (var detalle2 in listHeader2)
                {
                    if (xFila == 1)
                    {
                        CellMaster = new PdfPCell(new Phrase("Item", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        CellMaster.Rotation = 90;
                        tbl.AddCell(CellMaster);
                        CellMaster = new PdfPCell(new Phrase("Código", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        CellMaster.Colspan = 2;
                        tbl.AddCell(CellMaster);
                        CellMaster = new PdfPCell(new Phrase("Descripción", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        CellMaster.Colspan = 6;
                        tbl.AddCell(CellMaster);
                        CellMaster = new PdfPCell(new Phrase("M/P", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        CellMaster.Rotation = 90;
                        tbl.AddCell(CellMaster);
                        CellMaster = new PdfPCell(new Phrase("R/F/C", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        CellMaster.Rotation = 90;
                        tbl.AddCell(CellMaster);
                    }

                    CellMaster = new PdfPCell(new Phrase(detalle2.CodigoEquipo, parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    CellMaster.Rotation = 90;
                    tbl.AddCell(CellMaster);

                    xFila = xFila + 1;
                }

                xFila = 1;
                xSecuencia = 1;
                foreach (var detalle2 in item.TxRegistroEquipoDetalle2)
                {
                    if (xFila == 1)
                    {
                        CellMaster = new PdfPCell(new Phrase(xSecuencia.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(CellMaster);
                        CellMaster = new PdfPCell(new Phrase(detalle2.CodigoRepuesto, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                        CellMaster.Colspan = 2;
                        tbl.AddCell(CellMaster);
                        CellMaster = new PdfPCell(new Phrase(detalle2.Descripcion, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                        CellMaster.Colspan = 6;
                        CellMaster.FixedHeight = 25f;
                        tbl.AddCell(CellMaster);
                        CellMaster = new PdfPCell(new Phrase(detalle2.Mp, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(CellMaster);
                        CellMaster = new PdfPCell(new Phrase(detalle2.Rfc, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                        tbl.AddCell(CellMaster);
                        xSecuencia = xSecuencia + 1;
                    }

                    CellMaster = new PdfPCell(new Phrase(detalle2.FlgValor ? "a" : "", parrafoNegroWebdings)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    CellMaster.Rotation = 0;
                    if (!detalle2.FlgValor)
                    {
                        CellMaster.BackgroundColor = BaseColor.Red;
                    }
                    tbl.AddCell(CellMaster);

                    if (xFila == CountItemHeader)
                    {
                        xFila = 0;
                    }

                    xFila = xFila + 1;
                }

                doc.Add(tbl);

                doc.Add(new Phrase(" "));

                doc.Add(new Phrase("Resumen de mantenimiento", subTitulo));

                doc.Add(new Phrase(" "));

                tbl = new PdfPTable(new float[] { 5f, 15f, 15f, 65f }) { WidthPercentage = 100 };
                CellMaster = new PdfPCell();

                CellMaster = new PdfPCell(new Phrase("Item", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("Código", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("Activo", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("Observación", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                xSecuencia = 1;
                foreach (var detalle5 in item.TxRegistroEquipoDetalle5)
                {
                    CellMaster = new PdfPCell(new Phrase(xSecuencia.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle5.CodigoRepuesto, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle5.CodigoEquipo, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle5.Observacion, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    xSecuencia = xSecuencia + 1;
                }

                doc.Add(tbl);

                doc.Add(new Phrase("\n"));

                doc.Add(new Phrase("C. Requerimientos para los equipos y condiciones de limpieza", subTitulo));

                doc.Add(new Phrase(" "));

                tbl = new PdfPTable(new float[] { 8f, 72f, 10f, 10f }) { WidthPercentage = 80f };
                CellMaster = new PdfPCell();

                CellMaster = new PdfPCell(new Phrase("Item", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("Requerimientos para los equipos", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("SI", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("NO", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);

                foreach (var detalle4 in item.TxRegistroEquipoDetalle4)
                {
                    CellMaster = new PdfPCell(new Phrase("1", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle4.Descripcion, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle4.FlgValor ? "X" : "", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(!detalle4.FlgValor ? "X" : "", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                }

                doc.Add(tbl);

                doc.Add(new Phrase(" "));

                tbl = new PdfPTable(new float[] { 8f, 72f, 10f, 10f }) { WidthPercentage = 80f };
                CellMaster = new PdfPCell();

                CellMaster = new PdfPCell(new Phrase("Item", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("Limpieza y esterilización", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("SI", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("NO", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);

                foreach (var detalle3 in item.TxRegistroEquipoDetalle3)
                {
                    CellMaster = new PdfPCell(new Phrase("1", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle3.Descripcion, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle3.FlgValor ? "X" : "", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(!detalle3.FlgValor ? "X" : "", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                }

                doc.Add(tbl);

                doc.Add(new Phrase("\n"));

                doc.Add(new Phrase("D. Inventario de consumibles y repuestos", subTitulo));

                doc.Add(new Phrase(" "));

                tbl = new PdfPTable(new float[] { 6f, 15f, 63f, 12f, 12f, 12f }) { WidthPercentage = 100f };
                CellMaster = new PdfPCell();

                CellMaster = new PdfPCell(new Phrase("Item", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("Código", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("Descripción", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("Stock Actual", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("Cambio por mtto.", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);
                CellMaster = new PdfPCell(new Phrase("Entregado", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(CellMaster);

                xSecuencia = 1;
                foreach (var detalle6 in item.TxRegistroEquipoDetalle6)
                {
                    CellMaster = new PdfPCell(new Phrase(xSecuencia.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle6.CodigoRepuesto, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle6.Descripcion, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle6.StockActual.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle6.CambioPorMantenimiento.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);
                    CellMaster = new PdfPCell(new Phrase(detalle6.Entregado.ToString(), parrafoNegro)) { HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE };
                    tbl.AddCell(CellMaster);

                    xSecuencia = xSecuencia + 1;
                }

                doc.Add(tbl);

                doc.Add(new Phrase("\n"));

                doc.Add(new Phrase("E. Observaciones", subTitulo));

                doc.Add(new Phrase(" "));

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Observaciones Invetsa:", parrafoNegrita);
                c1.Border = 0;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.ObservacionesInvetsa, parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Observaciones Planta:", parrafoNegrita);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.ObservacionesPlanta, parrafoNegro);
                tbl.AddCell(c1);
                doc.Add(tbl);

                // Validamos si ingresaron imanes

                var countFotos = item.TxRegistroEquipoDetalle7.Count;
                iTextSharp.text.Image foto = null;

                if (countFotos > 0)
                {
                    doc.Add(new Phrase("\n"));

                    doc.Add(new Phrase("F. Fotos", subTitulo));

                    doc.Add(new Phrase(" "));

                    tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f };
                    CellMaster = new PdfPCell();

                    foreach (var detalle7 in item.TxRegistroEquipoDetalle7)
                    {
                        foto = ImagenBase64ToImagen(detalle7.Foto, 270f, 200f);

                        if (foto != null)
                        {
                            CellMaster = new PdfPCell(foto);
                            CellMaster.HorizontalAlignment = Element.ALIGN_CENTER;
                            CellMaster.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellMaster.Border = 0;
                            tbl.AddCell(CellMaster);
                        } 
                    }

                    if ((countFotos % 2) != 0)
                    {
                        CellMaster = new PdfPCell(new Phrase(" "));
                        CellMaster.Border = 0;
                        tbl.AddCell(CellMaster);
                    }

                    doc.Add(tbl);
                }

                doc.Add(new Phrase("\n"));
                doc.Add(new Phrase("\n"));

                tbl = new PdfPTable(new float[] { 45f, 10f, 45f }) { WidthPercentage = 100f };
                CellMaster = new PdfPCell();

                foto = ImagenBase64ToImagen(item.FirmaIncuba, 270f, 100f);

                CellMaster = new PdfPCell(foto);
                CellMaster.HorizontalAlignment = Element.ALIGN_CENTER;
                CellMaster.VerticalAlignment = Element.ALIGN_MIDDLE;
                CellMaster.Border = 0;
                tbl.AddCell(CellMaster);

                CellMaster = new PdfPCell(new Phrase(" "));
                CellMaster.Border = 0;
                tbl.AddCell(CellMaster);

                foto = ImagenBase64ToImagen(item.FirmaPlanta, 270f, 100f);

                CellMaster = new PdfPCell(foto);
                CellMaster.HorizontalAlignment = Element.ALIGN_CENTER;
                CellMaster.VerticalAlignment = Element.ALIGN_MIDDLE;
                CellMaster.Border = 0;
                tbl.AddCell(CellMaster);

                CellMaster = new PdfPCell(new Phrase("Jefe de planta de incubación", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                CellMaster.Border = 0;
                CellMaster.BorderWidthTop = 2f;
                tbl.AddCell(CellMaster);

                CellMaster = new PdfPCell(new Phrase(" "));
                CellMaster.Border = 0;
                tbl.AddCell(CellMaster);

                CellMaster = new PdfPCell(new Phrase("Responsable de Invetsa", parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                CellMaster.Border = 0;
                CellMaster.BorderWidthTop = 2f;
                tbl.AddCell(CellMaster);

                CellMaster = new PdfPCell(new Phrase("Nombre: " + item.ResponsablePlanta, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                CellMaster.Border = 0;
                tbl.AddCell(CellMaster);

                CellMaster = new PdfPCell(new Phrase(" "));
                CellMaster.Border = 0;
                tbl.AddCell(CellMaster);

                CellMaster = new PdfPCell(new Phrase("Nombre: " + item.ResponsableIncuba, parrafoNegro)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                CellMaster.Border = 0;
                tbl.AddCell(CellMaster);

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
    }
}
