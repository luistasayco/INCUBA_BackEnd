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
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Net.Data
{
    public class TxVacunacionSubCutaneaRepository : RepositoryBase<BE_TxVacunacionSubCutanea>, ITxVacunacionSubCutaneaRepository
    {

        //private readonly string _cnx;
        //private readonly IConfiguration _configuration;
        private string _aplicacionName;
        private string _metodoName;
        private readonly Regex regex = new Regex(@"<(\w+)>.*");

        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaPorFiltros";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaPorId";
        const string SP_GET_ID_NEW = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaPorIdNew";
        const string SP_GET_ID_GOOGLE_DRIVE = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaPorIdGoogleDrive";
        const string SP_GET_ID_DETALLE_NEW = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaDetallePorIdNew";
        const string SP_GET_ID_DETALLE = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaDetallePorId";
        const string SP_GET_ID_DETALLE_FOTOS = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaFotosPorId";
        const string SP_GET_ID_DETALLE_MAQUINA = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaMaquinaPorId";
        const string SP_GET_ID_DETALLE_VACUNA = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaVacunaPorId";
        const string SP_GET_ID_DETALLE_CONTROL_EFICIENCIA = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaControlEficienciaPorId";
        const string SP_GET_ID_DETALLE_IRREGULARIDAD = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaIrregularidadPorId";
        const string SP_GET_ID_DETALLE_IRREGULARIDAD_PDF = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaIrregularidadPorIdPdf";
        const string SP_GET_ID_DETALLE_RESULTADO_NEW = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaResultadoPorIdNew";
        const string SP_GET_ID_DETALLE_RESULTADO = DB_ESQUEMA + "INC_GetINC_TxVacunacionSubCutaneaResultadoPorId";
        const string SP_GET_ID_DETALLE_PROMEDIO = DB_ESQUEMA + "INC_GetTxVacunacionSubCutaneaPromedioPorId";

        const string SP_INSERT = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaInsert";
        const string SP_MERGE_DETALLE = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaDetalleMerge";
        const string SP_MERGE_DETALLE_FOTO = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaFotosMerge";
        const string SP_MERGE_DETALLE_MAQUINA = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaMaquinaMerge";
        const string SP_MERGE_DETALLE_VACUNA = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaVacunaMerge";
        const string SP_MERGE_DETALLE_IRREGULARIDAD = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaIrregularidadMerge";
        const string SP_MERGE_DETALLE_CONTROL_EFICIENCIA = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaControlEficienciaMerge";
        const string SP_MERGE_DETALLE_CONTROL_RESULTADO = DB_ESQUEMA + "INC_SetINC_TxVacunacionSubCutaneaResultadoMerge";
        const string SP_MERGE_DETALLE_PROMEDIO = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaPromedioMerge";

        const string SP_UPDATE = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaUpdate";
        const string SP_UPDATE_STATUS = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaStatusUpdate";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetTxVacunacionSubCutaneaDelete";


        const string SP_GET_MAESTRO_IRREGULARIDAD = DB_ESQUEMA + "INC_GetIrregularidadAll";
        public TxVacunacionSubCutaneaRepository(IConnectionSQL context)
            : base(context)
        {
            _aplicacionName = this.GetType().Name;
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
                    p.ListarTxVacunacionSubCutaneaControlEficiencia = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaControlEficiencia>(SP_GET_ID_DETALLE_CONTROL_EFICIENCIA, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaDetalle = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaDetalle>(SP_GET_ID_DETALLE, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaFotos = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaFotos>(SP_GET_ID_DETALLE_FOTOS, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaIrregularidad = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaIrregularidad>(SP_GET_ID_DETALLE_IRREGULARIDAD, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaMaquina = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaMaquina>(SP_GET_ID_DETALLE_MAQUINA, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaVacuna = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaVacuna>(SP_GET_ID_DETALLE_VACUNA, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaResultado = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaResultado>(SP_GET_ID_DETALLE_RESULTADO, entidad).ToList();
                    p.ListarTxVacunacionSubCutaneaPromedio = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaPromedio>(SP_GET_ID_DETALLE_PROMEDIO, entidad).ToList();
                }
                else
                {
                    p = new BE_TxVacunacionSubCutanea();
                }
                return p;
            });
            return objListPrincipal;
        }
        public Task<BE_TxVacunacionSubCutanea> GetByIdNew(BE_TxVacunacionSubCutanea entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_TxVacunacionSubCutanea p = context.ExecuteSqlViewId<BE_TxVacunacionSubCutanea>(SP_GET_ID_NEW, entidad);
                p.ListarTxVacunacionSubCutaneaDetalle = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaDetalle>(SP_GET_ID_DETALLE_NEW, entidad).ToList();
                p.ListarTxVacunacionSubCutaneaResultado = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaResultado>(SP_GET_ID_DETALLE_RESULTADO_NEW, entidad).ToList();
                return p;
            });
            return objListPrincipal;
        }
        public async Task<BE_ResultadoTransaccion> Create(BE_TxVacunacionSubCutanea value)
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

                            using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_CONTROL_RESULTADO, conn))
                            {
                                foreach (BE_TxVacunacionSubCutaneaResultado item in value.ListarTxVacunacionSubCutaneaResultado)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutaneaDetalle", item.IdVacunacionSubCutaneaDetalle));
                                    cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea));
                                    cmd.Parameters.Add(new SqlParameter("@IdProcesoAgrupador", item.IdProcesoAgrupador));
                                    cmd.Parameters.Add(new SqlParameter("@ValorEsperado", item.ValorEsperado));
                                    cmd.Parameters.Add(new SqlParameter("@ValorObtenido", item.ValorObtenido));
                                    cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                    cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            if (value.ListarTxVacunacionSubCutaneaControlEficiencia != null)
                            {
                                if (value.ListarTxVacunacionSubCutaneaControlEficiencia.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_CONTROL_EFICIENCIA, conn))
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
                                            cmd.Parameters.Add(new SqlParameter("@PorcentajeEficiencia", item.PorcentajeEficiencia));
                                            cmd.Parameters.Add(new SqlParameter("@PuntajeEficiencia", item.PuntajeEficiencia));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }

                            if (value.ListarTxVacunacionSubCutaneaPromedio != null)
                            {
                                // Promedio de Vacunacion SubCutanea
                                if (value.ListarTxVacunacionSubCutaneaPromedio.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_PROMEDIO, conn))
                                    {
                                        foreach (BE_TxVacunacionSubCutaneaPromedio item in value.ListarTxVacunacionSubCutaneaPromedio)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                            cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutaneaDetalle", item.IdVacunacionSubCutaneaDetalle));
                                            cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea));
                                            cmd.Parameters.Add(new SqlParameter("@NombreVacunador", item.NombreVacunador));
                                            cmd.Parameters.Add(new SqlParameter("@VacunadoPorHora", item.VacunadoPorHora));
                                            cmd.Parameters.Add(new SqlParameter("@PuntajeProductividad", item.PuntajeProductividad));
                                            cmd.Parameters.Add(new SqlParameter("@Controlados", item.Controlados));
                                            cmd.Parameters.Add(new SqlParameter("@SinVacunar", item.SinVacunar));
                                            cmd.Parameters.Add(new SqlParameter("@Heridos", item.Heridos));
                                            cmd.Parameters.Add(new SqlParameter("@Mojados", item.Mojados));
                                            cmd.Parameters.Add(new SqlParameter("@MalaPosicion", item.MalaPosicion));
                                            cmd.Parameters.Add(new SqlParameter("@VacunadoCorrectos", item.VacunadoCorrectos));
                                            cmd.Parameters.Add(new SqlParameter("@PorcentajeEficiencia", item.PorcentajeEficiencia));
                                            cmd.Parameters.Add(new SqlParameter("@PuntajeEficiencia", item.PuntajeEficiencia));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }

                            if (value.ListarTxVacunacionSubCutaneaIrregularidad != null)
                            {
                                if (value.ListarTxVacunacionSubCutaneaIrregularidad.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_IRREGULARIDAD, conn))
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
                                }
                            }

                            if (value.ListarTxVacunacionSubCutaneaMaquina != null)
                            {
                                if (value.ListarTxVacunacionSubCutaneaMaquina.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_MAQUINA, conn))
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
                                }
                            }

                            if (value.ListarTxVacunacionSubCutaneaVacuna != null)
                            {
                                if (value.ListarTxVacunacionSubCutaneaVacuna.Count() > 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand(SP_MERGE_DETALLE_VACUNA, conn))
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
                                }
                            }

                            if (value.ListarTxVacunacionSubCutaneaFotos != null)
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
                                            cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea));
                                            cmd.Parameters.Add(new SqlParameter("@Foto", item.Foto));
                                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                            await cmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }

                            transaction.Commit();

                            vResultadoTransaccion.IdRegistro = (int)value.IdVacunacionSubCutanea;
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
                            using (SqlCommand cmd = new SqlCommand(SP_UPDATE, conn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.Add(new SqlParameter("@IdVacunacionSubCutanea", value.IdVacunacionSubCutanea));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();
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

            MemoryStream memory = new MemoryStream();
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


            memory.Position = 0;
            var data = context.ExecuteSqlViewId<BE_TxVacunacionSubCutanea>(SP_GET_ID_GOOGLE_DRIVE, new BE_TxVacunacionSubCutanea { IdVacunacionSubCutanea = entidad.IdVacunacionSubCutanea });
            var nameFile = string.Format("{0}.{1}", data.NombreArchivo, "pdf");

            try
            {
                EmailSenderRepository emailSenderRepository = new EmailSenderRepository(context);
                var mensaje = string.Format("Se envía informe de Vacunación SubCutanea - N° {0}", entidad.IdVacunacionSubCutanea);
                await emailSenderRepository.SendEmailAsync(data.EmailTo, "Correo Automatico - Vacunación SubCutanea", mensaje, new BE_MemoryStream { FileMemoryStream = memory}, nameFile);
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

            var listaIrregularidad = context.ExecuteSqlViewFindByCondition<BE_TxVacunacionSubCutaneaIrregularidadPDF>(SP_GET_ID_DETALLE_IRREGULARIDAD_PDF, entidad).ToList();
            var listaIrregularidadMaestro = context.ExecuteSqlViewFindByCondition<BE_Irregularidad>(SP_GET_MAESTRO_IRREGULARIDAD, new BE_Irregularidad { DescripcionIrregularidad = "" }).ToList();

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
                iTextSharp.text.Font tituloBlanco = new iTextSharp.text.Font(helvetica, 18f, iTextSharp.text.Font.NORMAL, BaseColor.White);
                iTextSharp.text.Font subTitulo = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, BaseColor.White);
                iTextSharp.text.Font parrafoBlanco = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, BaseColor.White);
                iTextSharp.text.Font parrafoNegrita = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.BOLD, BaseColor.Black);
                iTextSharp.text.Font parrafoNegroLeyenda = new iTextSharp.text.Font(helvetica, 8f, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font parrafoNegro = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                iTextSharp.text.Font parrafoRojo = new iTextSharp.text.Font(helvetica, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Red);
                iTextSharp.text.Font parrafoNegroWebdings = new iTextSharp.text.Font(webdings, 10f, iTextSharp.text.Font.NORMAL, BaseColor.Black);
                pe.HeaderLeft = " ";
                pe.HeaderFont = parrafoBlanco;
                pe.HeaderRight = " ";
                doc.Open();

                var tblTitulo = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };

                var title = string.Format("VACUNACIÓN SUBCUTÁNEA - {0}", entidad.IdVacunacionSubCutanea, titulo);
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
                c1.Phrase = new Phrase("HY LINE", Boolean.Parse(item.FlgHyLine.ToString()) ? parrafoNegrita : parrafoNegro);
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

                tbl = new PdfPTable(new float[] { 30f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 20f, 15f }) { WidthPercentage = 100f };
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
                c1.Phrase = new Phrase("Pollos/día(promedio)", parrafoNegro);
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
                c1.Phrase = new Phrase("SUBCUTÁNEA", subTitulo);
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

                foreach (BE_TxVacunacionSubCutaneaMaquina itemMaquina in item.ListarTxVacunacionSubCutaneaMaquina)
                {
                    c1.Phrase = new Phrase(itemMaquina.DescripcionAguja, parrafoNegro);
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

                if (item.ListarTxVacunacionSubCutaneaVacuna != null)
                {
                    foreach (BE_TxVacunacionSubCutaneaVacuna itemVacuna in item.ListarTxVacunacionSubCutaneaVacuna)
                    {
                        c1.Phrase = new Phrase(itemVacuna.DescripcionVacuna, parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(itemVacuna.NombreVacuna, parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl.AddCell(c1);
                    }
                }
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 15f, 5f,10f, 50f, 15f,15f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Antibiótico:", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(Boolean.Parse(item.FlgAntibiotico.ToString()) ? "SI" : "NO", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Nombre: ", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.NombreAntibiotico, parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Dosis: ", parrafoNegro);
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(item.DosisAntibiotico, parrafoNegro);
                tbl.AddCell(c1);
                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 6;
                tbl.AddCell(c1);
                doc.Add(tbl);

                var proceso = "";
                var countDetalleNroQuiebreDos = 0;
                tbl = new PdfPTable(new float[] { 30f, 10f, 10f, 30f, 10f, 10f }) { WidthPercentage = 100f };

                foreach (BE_TxVacunacionSubCutaneaDetalle itemDetalle in item.ListarTxVacunacionSubCutaneaDetalle.Where(x => x.NroQuiebre == 2))
                {
                    if (proceso != itemDetalle.DescripcionProcesoSubCutanea)
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
                        c1.Phrase = new Phrase(itemDetalle.DescripcionProcesoSubCutanea, subTitulo);
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

                    c1.Phrase = new Phrase(itemDetalle.DescripcionProcesoDetalleSubCutanea, segunCondicionFormatoLeta);
                    c1.HorizontalAlignment = Element.ALIGN_MIDDLE;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemDetalle.Valor ? "X" : "", segunCondicionFormatoLeta);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemDetalle.Valor ? "" : "X", segunCondicionFormatoLeta);
                    tbl.AddCell(c1);

                    proceso = itemDetalle.DescripcionProcesoSubCutanea;
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

                foreach (BE_TxVacunacionSubCutaneaDetalle itemDetalle in item.ListarTxVacunacionSubCutaneaDetalle.Where(x => x.NroQuiebre == 1))
                {
                    if (proceso != itemDetalle.DescripcionProcesoSubCutanea)
                    {
                        c1 = new PdfPCell();
                        c1.Phrase = new Phrase(itemDetalle.DescripcionProcesoSubCutanea, subTitulo);
                        c1.BackgroundColor = new BaseColor(103, 93, 152);
                        c1.PaddingBottom = 8f;
                        c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        c1.Colspan = 2;
                        tbl.AddCell(c1);
                        c1 = new PdfPCell();
                        c1.Phrase = new Phrase("Asignar si el procedimiento estuviese siendo seguido, puntaje máximo " + itemDetalle.ValorProcesoSubCutanea.ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase("Puntaje", parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);

                    }

                    c1.Phrase = new Phrase(itemDetalle.DescripcionProcesoDetalleSubCutanea, parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemDetalle.Valor ? itemDetalle.ValorProcesoDetalleSubCutanea.ToString() : "", parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);

                    proceso = itemDetalle.DescripcionProcesoSubCutanea;
                }
                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 2;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 90f, 10f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("2.- MANEJO DE PAJILLA CONTROL DENTRO DEL TANQUE DE NITRÓGENO LÍQUIDO", subTitulo);
                c1.BackgroundColor = new BaseColor(103, 93, 152);
                c1.PaddingBottom = 8f;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                c1.Colspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Mantener una (1) pajilla invertida por cada canastilla como medida de control de cadena de frío", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("0.5", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Cumplimiento", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(Boolean.Parse(item.FlgPorcentajeViabilidad.ToString()) ? "SI" : "NO", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Colspan = 2;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 17f, 15f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 8f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("3.- MANTENIMIENTO DE LIMPIEZA DE LA VACUNADORAS SUBCUTÁNEA", subTitulo);
                c1.BackgroundColor = new BaseColor(103, 93, 152);
                c1.PaddingBottom = 8f;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                c1.Colspan = 13;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Puntaje Máximo 1.0", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                c1.Colspan = 13;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                c1.Colspan = 13;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1 = new PdfPCell(new Phrase("Nombre Vacunador", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("N° de", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);

                c1 = new PdfPCell(new Phrase("*Irregularidades", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Colspan = 10;
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("Puntaje", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                c1.Rowspan = 2;
                tbl.AddCell(c1);
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
                c1 = new PdfPCell(new Phrase("6", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("7", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("8", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("9", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);
                c1 = new PdfPCell(new Phrase("10", parrafoBlanco)) { BackgroundColor = new BaseColor(103, 93, 152), HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                tbl.AddCell(c1);

                foreach (BE_TxVacunacionSubCutaneaIrregularidadPDF itemControl in listaIrregularidad)
                {
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.NombreVacunador.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.CodigoEquipo.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Irregularidad_1 > 0 ? "a" : "", parrafoNegroWebdings);
                    if (itemControl.Irregularidad_1.Equals(0))
                    {
                        c1.BackgroundColor = BaseColor.Red;
                    }
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Irregularidad_2 > 0 ? "a" : "", parrafoNegroWebdings);
                    if (itemControl.Irregularidad_2.Equals(0))
                    {
                        c1.BackgroundColor = BaseColor.Red;
                    }
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Irregularidad_3 > 0 ? "a" : "", parrafoNegroWebdings);
                    if (itemControl.Irregularidad_3.Equals(0))
                    {
                        c1.BackgroundColor = BaseColor.Red;
                    }
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Irregularidad_4 > 0 ? "a" : "", parrafoNegroWebdings);
                    if (itemControl.Irregularidad_4.Equals(0))
                    {
                        c1.BackgroundColor = BaseColor.Red;
                    }
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Irregularidad_5 > 0 ? "a" : "", parrafoNegroWebdings);
                    if (itemControl.Irregularidad_5.Equals(0))
                    {
                        c1.BackgroundColor = BaseColor.Red;
                    }
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Irregularidad_6 > 0 ? "a" : "", parrafoNegroWebdings);
                    if (itemControl.Irregularidad_6.Equals(0))
                    {
                        c1.BackgroundColor = BaseColor.Red;
                    }
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Irregularidad_7 > 0 ? "a" : "", parrafoNegroWebdings);
                    if (itemControl.Irregularidad_7.Equals(0))
                    {
                        c1.BackgroundColor = BaseColor.Red;
                    }
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Irregularidad_8 > 0 ? "a" : "", parrafoNegroWebdings);
                    if (itemControl.Irregularidad_8.Equals(0))
                    {
                        c1.BackgroundColor = BaseColor.Red;
                    }
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Irregularidad_9 > 0 ? "a" : "", parrafoNegroWebdings);
                    if (itemControl.Irregularidad_9.Equals(0))
                    {
                        c1.BackgroundColor = BaseColor.Red;
                    }
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Irregularidad_10 > 0 ? "a" : "", parrafoNegroWebdings);
                    if (itemControl.Irregularidad_10.Equals(0))
                    {
                        c1.BackgroundColor = BaseColor.Red;
                    }
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.Puntaje.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                }

                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 13;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("*Irregularidades", parrafoNegrita);
                c1.Border = 0;
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl.AddCell(c1);

                foreach (BE_Irregularidad item in listaIrregularidadMaestro)
                {
                    c1.Phrase = new Phrase(string.Format("{0} - {1}", item.IdIrregularidad.ToString() , item.DescripcionIrregularidad), parrafoNegroLeyenda);
                    c1.Border = 0;
                    tbl.AddCell(c1);
                }
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 20f, 8f, 8f, 8f, 8f, 8f, 8f, 8f, 8f, 8f, 8f}) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("4.- CONTROL DE INDICE DE EFICIENCIA DE VACUNACIÓN", subTitulo);
                c1.BackgroundColor = new BaseColor(103, 93, 152);
                c1.PaddingBottom = 8f;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                c1.Colspan = 11;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Indice de eficiencia aceptable mayor a 99%", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                c1.Colspan = 7;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Puntaje Máximo 5.5", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                c1.Colspan = 4;
                tbl.AddCell(c1);
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Vacunador", parrafoNegro);
                c1.Rotation = 0;
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("N° Pollos Vac/Hora", parrafoNegro);
                c1.Rotation = 90;
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Puntaje", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Controlados", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Vacunados", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Heridos", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Mojados", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Mala Posición", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Vac. Correctamente", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("% de eficiencia", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("Puntaje Indice", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);

                foreach (BE_TxVacunacionSubCutaneaControlEficiencia itemControl in item.ListarTxVacunacionSubCutaneaControlEficiencia)
                {
                    c1 = new PdfPCell();
                    c1.Phrase = new Phrase(itemControl.NombreVacunador.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.VacunadoPorHora.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.PuntajeProductividad.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.Controlados.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.SinVacunar.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.Heridos.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.Mojados.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.MalaPosicion.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.VacunadoCorrectos.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.PorcentajeEficiencia.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                    c1.Phrase = new Phrase(itemControl.PuntajeEficiencia.ToString(), parrafoNegro);
                    c1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl.AddCell(c1);
                }

                BE_TxVacunacionSubCutaneaPromedio modeloPromedio = new BE_TxVacunacionSubCutaneaPromedio();

                foreach (BE_TxVacunacionSubCutaneaPromedio itemControl in item.ListarTxVacunacionSubCutaneaPromedio)
                {
                    if (!itemControl.NombreVacunador.Trim().Equals("Sumatoria"))
                    {
                        c1 = new PdfPCell();
                        c1.Phrase = new Phrase(itemControl.NombreVacunador.ToString(), parrafoNegro);
                        c1.BackgroundColor = new BaseColor(184, 182, 181);
                        c1.HorizontalAlignment = Element.ALIGN_LEFT;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(decimal.Round(itemControl.VacunadoPorHora, 1).ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(decimal.Round(itemControl.PuntajeProductividad, 1).ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(decimal.Round(itemControl.Controlados, 1).ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(decimal.Round(itemControl.SinVacunar, 1).ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(decimal.Round(itemControl.Heridos, 1).ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(decimal.Round(itemControl.Mojados, 1).ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(decimal.Round(itemControl.MalaPosicion, 1).ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(decimal.Round(itemControl.VacunadoCorrectos, 1).ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(decimal.Round(itemControl.PorcentajeEficiencia, 1).ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);
                        c1.Phrase = new Phrase(decimal.Round(itemControl.PuntajeEficiencia, 1).ToString(), parrafoNegro);
                        c1.HorizontalAlignment = Element.ALIGN_CENTER;
                        tbl.AddCell(c1);

                    }
                    if (itemControl.NombreVacunador.Trim().Equals("Promedio"))
                    {
                        modeloPromedio = itemControl;
                    }
                }

               

                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 11;
                tbl.AddCell(c1);
                doc.Add(tbl);

                tbl = new PdfPTable(new float[] { 50f, 25f, 25f }) { WidthPercentage = 100f };
                c1 = new PdfPCell();
                c1.Phrase = new Phrase("5.- PRODUCTIVIDAD", subTitulo);
                c1.BackgroundColor = new BaseColor(103, 93, 152);
                c1.PaddingBottom = 8f;
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                c1.Colspan = 3;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Productividad: Pollos vacunados en 1 hora (Puntaje máximo 1.0) ", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                c1.Colspan = 2;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Puntaje", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl.AddCell(c1);

                c1 = new PdfPCell();
                c1.Phrase = new Phrase("Resultado dentro de -10% de la media", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl.AddCell(c1);
                //c1.Phrase = new Phrase(string.Format("({0} - {1})", decimal.Round(modeloPromedio.VacunadoPorHora - (modeloPromedio.VacunadoPorHora * 0.1M),0), decimal.Round(modeloPromedio.VacunadoPorHora + (modeloPromedio.VacunadoPorHora * 0.5M)),0), parrafoNegro);
                c1.Phrase = new Phrase(string.Format("({0}>)", decimal.Round(modeloPromedio.VacunadoPorHora - (modeloPromedio.VacunadoPorHora * 0.1M), 0)), parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("1.0 punto", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);

                c1.Phrase = new Phrase("10 - 20 % por debajo de la media", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(string.Format("({0} - {1})", decimal.Round(modeloPromedio.VacunadoPorHora - (modeloPromedio.VacunadoPorHora * 0.2M),0), decimal.Round(modeloPromedio.VacunadoPorHora - (modeloPromedio.VacunadoPorHora * 0.1M)),0), parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("0.5 punto", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);

                c1.Phrase = new Phrase("21 % a mas por debajo de la media", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_LEFT;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase(string.Format("(<{0})", decimal.Round(modeloPromedio.VacunadoPorHora - (modeloPromedio.VacunadoPorHora * 0.2M)),0), parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);
                c1.Phrase = new Phrase("0.0 punto", parrafoNegro);
                c1.HorizontalAlignment = Element.ALIGN_CENTER;
                tbl.AddCell(c1);

                //Insertamos una linea en blanco en la tabla
                c1 = new PdfPCell();
                c1.Phrase = new Phrase(" ", parrafoNegro);
                c1.Border = 0;
                c1.Colspan = 3;
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

                foreach (BE_TxVacunacionSubCutaneaResultado itemResultado in item.ListarTxVacunacionSubCutaneaResultado)
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

                var countFotos = item.ListarTxVacunacionSubCutaneaFotos.Count();
                iTextSharp.text.Image foto = null;


                if (countFotos > 0)
                {

                    tbl = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f };
                    c1 = new PdfPCell();

                    foreach (BE_TxVacunacionSubCutaneaFotos itemFoto in item.ListarTxVacunacionSubCutaneaFotos)
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
