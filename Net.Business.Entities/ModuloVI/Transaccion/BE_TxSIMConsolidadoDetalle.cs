using System;
using System.Collections.Generic;
using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxSIMConsolidadoDetalle: EntityBase
    {
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdSIMConsolidadoDetalle { get; set; }
        public int IdSIMConsolidado { get; set; }
        public int IdSIM { get; set; }
        public string CodigoPlanta { get; set; }
        public int? Edad { get; set; }
        public string Sexo { get; set; }
        public string Zona { get; set; }
        public int? Galpon { get; set; }
        public int? NroPollos { get; set; }
        public DateTime? FecHoraRegistro { get; set; }
    }
}
