using Net.Connection.Attributes;
using System;
using System.Data;

namespace Net.Business.Entities
{
    public class FE_TxVacunacionSubCutanea: EntityBase
    {
        /// <summary>
        /// IdExamenFisico
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdVacunacionSubCutanea { get; set; }
        /// <summary>
        /// CodigoEmpresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEmpresa { get; set; }
        //Filtros
        /// <summary>
        /// FecRegistroInicio
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime FecRegistroInicio { get; set; }
        /// <summary>
        /// FecRegistroFin
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime FecRegistroFin { get; set; }
    }
}
