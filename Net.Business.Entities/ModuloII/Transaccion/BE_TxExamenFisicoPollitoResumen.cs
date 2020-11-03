using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxExamenFisicoPollitoResumen
    {
        /// <summary>
        /// IdExamenFisicoDetalle
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdExamenFisicoDetalle { get; set; }
        /// <summary>
        /// IdExamenFisico
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdExamenFisico { get; set; }
        /// <summary>
        /// IdProceso
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdProceso { get; set; }
        /// <summary>
        /// DescripcionProceso
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionProceso { get; set; }
        /// <summary>
        /// Esperado
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal Esperado { get; set; }
        /// <summary>
        /// Obtenido
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal Obtenido { get; set; }
    }
}
