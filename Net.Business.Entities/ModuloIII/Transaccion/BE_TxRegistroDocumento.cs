using Net.Connection.Attributes;
using System;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxRegistroDocumento: EntityBase
    {
        [DBParameter(SqlDbType.Int, ActionType.Everything, true)]
        public int? IdDocumento { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdSubTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionSubTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEmpresa { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionEmpresa { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoPlanta { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionPlanta { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? Ano { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? Mes { get; set; }
        [DBParameter(SqlDbType.VarChar, 300, ActionType.Everything)]
        public string IdGoogleDrive { get; set; }
        [DBParameter(SqlDbType.VarChar, 200, ActionType.Everything)]
        public string NombreArchivo { get; set; }
        [DBParameter(SqlDbType.VarChar, 100, ActionType.Everything)]
        public string TipoArchivo { get; set; }
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? FlgActivo { get; set; }
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecRegistro { get; set; }
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecHoraRegistro { get; set; }
        [DBParameter(SqlDbType.VarChar, 10, ActionType.Everything)]
        public string ExtencionArchivo { get; set; }

        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecInicio { get; set; }
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecFin { get; set; }
    }
}
