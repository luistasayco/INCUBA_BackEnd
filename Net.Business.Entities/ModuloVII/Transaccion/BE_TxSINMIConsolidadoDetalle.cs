using System;
using System.Collections.Generic;
using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxSINMIConsolidadoDetalle : EntityBase
    {
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdSINMIConsolidadoDetalle { get; set; }
        public int IdSINMIConsolidado { get; set; }
        public int IdSINMI { get; set; }
        public string CodigoPlanta { get; set; }
        public int? Edad { get; set; }
        public string MotivoVisita { get; set; }
        public DateTime? FecHoraRegistro { get; set; }
    }
}