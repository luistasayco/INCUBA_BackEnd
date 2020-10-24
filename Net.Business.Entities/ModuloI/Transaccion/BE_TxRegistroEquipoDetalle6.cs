using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxRegistroEquipoDetalle6 : EntityBase
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
        /// CodigoRepuesto
        /// </summary>
        [DBParameter(SqlDbType.NVarChar , 50, ActionType.Everything)]
        public string CodigoRepuesto { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
        /// <summary>
        /// StockActual
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int StockActual { get; set; }
        /// <summary>
        /// CambioPorMantenimiento
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int CambioPorMantenimiento { get; set; }
        /// <summary>
        /// Entregado
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Entregado { get; set; }
    }
}
