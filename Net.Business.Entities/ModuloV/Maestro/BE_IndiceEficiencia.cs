using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_IndiceEficiencia: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdIndiceEficiencia { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionIndiceEficiencia { get; set; }
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal? RangoInicial { get; set; }
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal? RangoFinal { get; set; }
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal? Puntaje { get; set; }
    }
}
