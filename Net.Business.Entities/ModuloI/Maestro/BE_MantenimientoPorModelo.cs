using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_MantenimientoPorModelo : EntityBase
    {
        /// <summary>
        /// Id de Mantenimiento
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdMantenimientoPorModelo { get; set; }
        /// <summary>
        /// Codigo de Modelo
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoModelo { get; set; }
        /// <summary>
        /// Id de Mantenimiento
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdMantenimiento { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
    }
}
