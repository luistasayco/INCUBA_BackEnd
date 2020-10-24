using Net.Connection.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Net.Business.Entities
{
    public class BE_ProcesoDetalle : EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true )]
        public int? IdProcesoDetalle { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdProceso { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal? Factor { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? Orden { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string TipoControl { get; set; }
    }
}
