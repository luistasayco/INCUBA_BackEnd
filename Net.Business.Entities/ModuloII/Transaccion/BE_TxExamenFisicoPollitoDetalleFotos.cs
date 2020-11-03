using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxExamenFisicoPollitoDetalleFotos 
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
        /// Foto
        /// </summary>
        [DBParameter(SqlDbType.Text, 0, ActionType.Everything)]
        public string Foto { get; set; }
    }
}
