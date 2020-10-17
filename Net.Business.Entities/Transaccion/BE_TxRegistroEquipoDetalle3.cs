using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxRegistroEquipoDetalle3 : EntityBase
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
        /// IdCondicionLimpieza
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdCondicionLimpieza { get; set; }
        /// <summary>
        /// IdCondicionLimpieza
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
        /// <summary>
        /// FlgValor
        /// </summary>
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public bool FlgValor { get; set; }
    }
}
