using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Boquilla : EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdBoquilla { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionBoquilla { get; set; }
    }
}
