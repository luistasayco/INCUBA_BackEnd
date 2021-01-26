using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Vacuna: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdVacuna { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionVacuna { get; set; }
    }
}
