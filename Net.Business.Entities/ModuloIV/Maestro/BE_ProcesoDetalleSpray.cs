﻿using Net.Connection.Attributes;
using System;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_ProcesoDetalleSpray : EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdProcesoDetalleSpray { get; set; }
        [DBParameter(SqlDbType.Int, 0 , ActionType.Everything)]
        public int? IdProcesoSpray { get; set; }
        [DBParameter(SqlDbType.NVarChar, 200, ActionType.Everything)]
        public string DescripcionProcesoSpray { get; set; }
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public decimal? Valor { get; set; }
    }
}
