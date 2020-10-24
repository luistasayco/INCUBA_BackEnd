using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    /// <summary>
    /// Entidad de Calidad
    /// </summary>
    public class BE_Calidad : EntityBase
    {
        /// <summary>
        /// Id de Calidad
        /// </summary>
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdCalidad { get; set; }
        /// <summary>
        /// Descripcion de Calidad
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
        /// <summary>
        /// Rango Inicial
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? RangoInicial { get; set; }
        /// <summary>
        /// Rango Final
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? RangoFinal { get; set; }
        /// <summary>
        /// Color Hexagedecimal
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 10, ActionType.Everything)]
        public string Color { get; set; }
    }
}
