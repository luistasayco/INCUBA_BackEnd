using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxRegistroEquipoDetalle7 : EntityBase
    {
        /// <summary>
        /// IdRegistroEquipoDetalle
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdRegistroEquipoDetalle { get; set; }
        /// <summary>
        /// IdRegistroEquipo
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdRegistroEquipo { get; set; }
        /// <summary>
        /// Foto
        /// </summary>
        [DBParameter(SqlDbType.Text, 0, ActionType.Everything)]
        public string Foto { get; set; }
        /// <summary>
        /// Orden
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Orden { get; set; }
    }
}
