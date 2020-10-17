using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_EquipoPorModelo: EntityBase
    {
        /// <summary>
        /// Id de Calidad
        /// </summary>
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdEquipoPorModelo { get; set; }
        /// <summary>
        /// CodigoEmpresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEmpresa { get; set; }
        /// <summary>
        /// CodigoEmpresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoPlanta { get; set; }
        /// <summary>
        /// CodigoEmpresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoModelo { get; set; }
        /// <summary>
        /// CodigoEmpresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEquipo { get; set; }
    }
}
