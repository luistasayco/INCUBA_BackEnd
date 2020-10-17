using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_General
    {
        /// <summary>
        /// Codigo Empresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEmpresa { get; set; }
        /// <summary>
        /// Codigo Planta
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoPlanta { get; set; }
        /// <summary>
        /// CodigoModelo
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoModelo { get; set; }

    }
}
