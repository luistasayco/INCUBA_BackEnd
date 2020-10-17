using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Net.Business.Entities;
using Net.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Transactions;

namespace Net.Data
{
    public class TxRegistroEquipoRepository : RepositoryBase<BE_TxRegistroEquipo>, ITxRegistroEquipoRepository
    {
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
        public async Task<int> Create(BE_TxRegistroEquipo value)
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

                                SqlParameter oParam = new SqlParameter("@IdRegistroEquipo", value.IdRegistroEquipo);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@CodigoEmpresa", value.CodigoEmpresa));
                                cmd.Parameters.Add(new SqlParameter("@CodigoPlanta", value.CodigoPlanta));
                                cmd.Parameters.Add(new SqlParameter("@CodigoModelo", value.CodigoModelo));
                                cmd.Parameters.Add(new SqlParameter("@FirmaIncuba", value.FirmaIncuba));
                                cmd.Parameters.Add(new SqlParameter("@FirmaPlanta", value.FirmaPlanta));
                                cmd.Parameters.Add(new SqlParameter("@FlgCerrado", value.FlgCerrado));
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
                            value.IdRegistroEquipo = 0;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                value.IdRegistroEquipo = 0;
            }

            return int.Parse(value.IdRegistroEquipo.ToString());
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
        public Task UpdateStatus(BE_TxRegistroEquipo entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE_STATUS));
        }
        public Task Delete(BE_TxRegistroEquipo entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
