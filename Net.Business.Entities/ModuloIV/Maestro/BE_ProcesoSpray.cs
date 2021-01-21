using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_ProcesoSpray: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdProcesoSpray { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionProcesoSpray { get; set; }
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal Valor { get; set; }
    }
}
