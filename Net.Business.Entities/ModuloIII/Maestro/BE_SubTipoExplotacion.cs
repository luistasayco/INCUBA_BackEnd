using Net.Connection.Attributes;
using System;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_SubTipoExplotacion : EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdSubTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.VarChar, 200, ActionType.Everything)]
        public string DescripcionSubTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.VarChar, 300, ActionType.Everything)]
        public string NombreDocumento { get; set; }
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? FlgRequiereFormato { get; set; }
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? FlgExisteDigital { get; set; }
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? FlgParaCliente { get; set; }
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? FlgParaInvetsa { get; set; }
    }
}
