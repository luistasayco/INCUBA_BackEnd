using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Planta
    {
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoPlanta { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEmpresa { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
    }
}
