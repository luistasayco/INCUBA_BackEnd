using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxExamenFisicoPollitoDetalle: EntityBase
    {
        /// <summary>
        /// Id Detalle
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdDetalle { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Id { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdProcesoDetalle { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Valor { get; set; }
    }
}
