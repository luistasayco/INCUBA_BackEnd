using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Irregularidad: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdIrregularidad { get; set; }
        [DBParameter(SqlDbType.NVarChar, 200, ActionType.Everything)]
        public string DescripcionIrregularidad { get; set; }
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal? Valor { get; set; }
    }
}
