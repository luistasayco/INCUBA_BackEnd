using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_ProcesoDetalleSubCutanea: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdProcesoDetalleSubCutanea { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdProcesoSubCutanea { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionProcesoSubCutanea { get; set; }
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal Valor { get; set; }
    }
}
