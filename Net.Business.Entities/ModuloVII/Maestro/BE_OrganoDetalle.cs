using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_OrganoDetalle: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdOrganoDetalle { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdOrgano { get; set; }
        [DBParameter(SqlDbType.NVarChar, 200, ActionType.Everything)]
        public string DescripcionOrganoDetalle { get; set; }
        [DBParameter(SqlDbType.NVarChar, 10, ActionType.Everything)]
        public string Score { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? OrdenDetalle { get; set; }
    }
}
