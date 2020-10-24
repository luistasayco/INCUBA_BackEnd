using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_RequerimientoEquipo: EntityBase 
    {
        /// <summary>
        /// Id de Requerimiento de equipo
        /// </summary>
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdRequerimientoEquipo { get; set; }
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
