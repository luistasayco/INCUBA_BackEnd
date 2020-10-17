using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Equipo
    {
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEmpresa { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoPlanta { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoModelo { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEquipo { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }

        //Variable local
        public string IdEquipo { get => string.Format("{0}{1}{2}{3}", CodigoEmpresa, CodigoPlanta, CodigoModelo, CodigoEquipo); }
    }
}
