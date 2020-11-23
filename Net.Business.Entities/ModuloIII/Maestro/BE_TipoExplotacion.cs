using Net.Connection.Attributes;
using System;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TipoExplotacion : EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.NVarChar, 200, ActionType.Everything)]
        public string DescripcionTipoExplotacion { get; set; }
    }
}
