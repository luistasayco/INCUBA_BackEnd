using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Organo: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdOrgano { get; set; }
        [DBParameter(SqlDbType.NVarChar, 200, ActionType.Everything)]
        public string DescripcionOrgano { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? Orden { get; set; }
    }
}
