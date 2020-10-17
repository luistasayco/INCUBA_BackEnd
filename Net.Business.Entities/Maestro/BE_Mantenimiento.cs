using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Mantenimiento : EntityBase
    {
        /// <summary>
        /// Id de Mantenimiento
        /// </summary>
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdMantenimiento { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
    }
}
