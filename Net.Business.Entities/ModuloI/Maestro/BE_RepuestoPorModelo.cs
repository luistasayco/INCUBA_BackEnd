using Net.Connection.Attributes;
using System;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_RepuestoPorModelo : EntityBase
    {
        /// <summary>
        /// Id de Repuesto por modelo
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdRepuestoPorModelo { get; set; }
        /// <summary>
        /// CodigoModelo
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoModelo { get; set; }
        /// <summary>
        /// Codigo Repuesto
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoRepuesto { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
        /// <summary>
        /// FlgPredeterminado
        /// </summary>
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? FlgPredeterminado { get; set; }
        /// <summary>
        /// FlgAccesorio
        /// </summary>
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? FlgAccesorio { get; set; }
    }
}
