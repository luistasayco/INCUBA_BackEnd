using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class EF_SubTipoExplotacion: EntityBase
    {
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdTipoExplotacion { get; set; }
    }
}
