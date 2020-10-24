using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Modelo : EntityBase
    {
        /// <summary>
        /// CodigoModelo
        /// </summary>
        [DBParameter(SqlDbType.NVarChar ,50, ActionType.Everything)]
        public string CodigoModelo { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
    }
}
