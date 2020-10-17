using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_CondicionLimpieza : EntityBase
    {
        /// <summary>
        /// Id de Condicion Limpieza
        /// </summary>
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdCondicionLimpieza { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
        /// <summary>
        /// Orden
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? Orden { get; set; }
    }
}
