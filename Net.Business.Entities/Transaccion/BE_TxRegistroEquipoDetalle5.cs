using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxRegistroEquipoDetalle5 : EntityBase
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
        /// Id Repuesto por Modelo
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdRepuestoPorModelo { get; set; }
        /// <summary>
        /// Observacion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoRepuesto { get; set; }
        /// <summary>
        /// Observacion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEquipo { get; set; }
        /// <summary>
        /// Observacion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 150, ActionType.Everything)]
        public string Descripcion { get; set; }
        /// <summary>
        /// Observacion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 150, ActionType.Everything)]
        public string Observacion { get; set; }
    }
}
