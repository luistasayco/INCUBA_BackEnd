using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Irregularidad: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdIrregularidad { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionIrregularidad { get; set; }
    }
}
