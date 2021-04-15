using System;
using System.Collections.Generic;
using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxSINMI : EntityBase
    {
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdSINMI { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public int? Edad { get; set; }
        public string MotivoVisita { get; set; }
        public DateTime? FecRegistro { get; set; }
        public DateTime? FecHoraRegistro { get; set; }
        public string ResponsableInvetsa { get; set; }
        public string ResponsableIncubadora { get; set; }
        public string ObservacionInvetsa { get; set; }
        public string ObservacionPlanta { get; set; }
        public bool? FlgCerrado { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdUsuarioCierre { get; set; }
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecCierre { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdSINMIConsolidado { get; set; }
        // Extras
        public string UsuarioCreacion { get; set; }
        public string UsuarioCierre { get; set; }
        public string NombreArchivo { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string DescripcionPlanta { get; set; }
        public string DescripcionTipoExplotacion { get; set; }
        public string DescripcionSubTipoExplotacion { get; set; }
        public int? IdSubTipoExplotacion { get; set; }

        public IEnumerable<BE_TxSINMIDetalle> ListaTxSINMIDetalle { get; set; }
        public IEnumerable<BE_TxSINMIFotos> ListaTxSINMIFotos { get; set; }
    }
}
