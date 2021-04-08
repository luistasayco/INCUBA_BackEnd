using Net.Connection.Attributes;
using System;
using System.Data;

namespace Net.Business.Entities
{
    public class FE_TxSIMConsolidadoCodigo
    {
        /// <summary>
        /// CodigoEmpresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEmpresa { get; set; }
    }
}
