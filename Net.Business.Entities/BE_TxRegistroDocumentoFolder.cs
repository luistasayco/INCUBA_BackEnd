using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxRegistroDocumentoFolder: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdFolder { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdSubTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEmpresa { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoPlanta { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? Ano { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? Mes { get; set; }
        [DBParameter(SqlDbType.NVarChar, 300, ActionType.Everything)]
        public string IdGoogleDrive { get; set; }

    }
}
