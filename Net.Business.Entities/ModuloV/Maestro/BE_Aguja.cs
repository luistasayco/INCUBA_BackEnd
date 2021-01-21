using Net.Connection.Attributes;
using System.Data;


namespace Net.Business.Entities
{
    public class BE_Aguja: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int IdAguja { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionAguja { get; set; }
    }
}
