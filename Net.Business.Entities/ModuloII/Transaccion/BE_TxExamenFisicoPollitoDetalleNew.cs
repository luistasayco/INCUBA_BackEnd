using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxExamenFisicoPollitoDetalleNew
    {
        /// <summary>
        /// IdExamenFisicoDetalle
        /// </summary>
        public int IdExamenFisicoDetalle { get; set; }
        /// <summary>
        /// NumeroPollito
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int NumeroPollito { get; set; }
        /// <summary>
        /// IdProceso
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdProceso { get; set; }
        /// <summary>
        /// NumeroPollito
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionProceso { get; set; }
        /// <summary>
        /// IdProcesoDetalle
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdProcesoDetalle { get; set; }
        /// <summary>
        /// NumeroPollito
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Id1 { get; set; }
        /// <summary>
        /// NumeroPollito
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion1 { get; set; }
        /// <summary>
        /// NumeroPollito
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Id2 { get; set; }
        /// <summary>
        /// NumeroPollito
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion2 { get; set; }
        /// <summary>
        /// NumeroPollito
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal Factor { get; set; }
        /// <summary>
        /// NumeroPollito
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal Valor { get; set; }
        /// <summary>
        /// NumeroPollito
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Orden { get; set; }
        /// <summary>
        /// ValorDefault
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int ValorDefault { get; set; }

    }
}
