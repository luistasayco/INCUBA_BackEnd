using System;
using System.Collections.Generic;
using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxSIM: EntityBase
    {
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdSIM { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public int? Edad { get; set; }
        public string Sexo { get; set; }
        public string Zona { get; set; }
        public int? Galpon { get; set; }
        public int? NroPollos { get; set; }
        public DateTime? FecRegistro { get; set; }
        public DateTime? FecHoraRegistro { get; set; }
        public string ResponsableInvetsa { get; set; }
        public string ResponsableIncubadora { get; set; }
        public int? RelacionAFavorBursa { get; set; }
        public int? RelacionAFavorBazo { get; set; }
        public int? Relacion1a1 { get; set; }
        public int? AparienciaNormal { get; set; }
        public int? AparienciaAnormal { get; set; }
        public int? TamanoNormal { get; set; }
        public int? TamanoAnormal { get; set; }
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
        public int? IdSIMConsolidado { get; set; }
        // Extras
        public string UsuarioCreacion { get; set; }
        public string UsuarioCierre { get; set; }
        public string NombreArchivo { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string DescripcionPlanta { get; set; }
        public string DescripcionTipoExplotacion { get; set; }
        public string DescripcionSubTipoExplotacion { get; set; }
        public int? IdSubTipoExplotacion { get; set; }

        public IEnumerable<BE_TxSIMDigestivo> ListaTxSIMDigestivos  { get; set; }
        public IEnumerable<BE_TxSIMFotos> ListaTxSIMFotos { get; set; }
        public IEnumerable<BE_TxSIMIndiceBursal> ListaTxSIMIndiceBursal { get; set; }
        public IEnumerable<BE_TxSIMLesionBursa> ListaTxSIMLesionBursa { get; set; }
        public IEnumerable<BE_TxSIMLesiones> ListaTxSIMLesiones { get; set; }
        public IEnumerable<BE_TxSIMLesionTimo> ListaTxSIMLesionTimo { get; set; }
        public IEnumerable<BE_TxSIMRespiratorio> ListaTxSIMRespiratorio { get; set; }

    }
}
