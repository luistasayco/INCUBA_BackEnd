using System;
using System.Collections.Generic;
using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxVacunacionSubCutanea: EntityBase
    {
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdVacunacionSubCutanea { get; set; }
        public string CodigoEmpresa { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public string DescripcionPlanta { get; set; }
        public string Unidad { get; set; }
        public DateTime? FecRegistro { get; set; }
        public DateTime? FecHoraRegistro { get; set; }
        public string ResponsableInvetsa { get; set; }
        public string ResponsableIncubadora { get; set; }
        public Boolean? FlgHyLine { get; set; }
        public Boolean? FlgLohman { get; set; }
        public Boolean? FlgRoss { get; set; }
        public Boolean? FlgCobb { get; set; }
        public Boolean? FlgOtros { get; set; }
        public string ObservacionOtros { get; set; }
        public Boolean? FlgLunes { get; set; }
        public Boolean? FlgMartes { get; set; }
        public Boolean? FlgMiercoles { get; set; }
        public Boolean? FlgJueves { get; set; }
        public Boolean? FlgViernes { get; set; }
        public Boolean? FlgSabado { get; set; }
        public Boolean? FlgDomingo { get; set; }
        public Boolean? FlgAntibiotico { get; set; }
        public string NombreAntibiotico { get; set; }
        public string DosisAntibiotico { get; set; }
        public Boolean? FlgPorcentajeViabilidad { get; set; }
        public decimal PuntajePorcentajeViabilidad { get; set; }
        public string ObservacionInvetsa { get; set; }
        public string ObservacionPlanta { get; set; }
        public decimal? PromedioPollos { get; set; }
        public string ResponsablePlanta { get; set; }
        public string FirmaInvetsa { get; set; }
        public string FirmaPlanta { get; set; }
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? FlgCerrado { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdUsuarioCierre { get; set; }
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecCierre { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string NombreArchivo { get; set; }
        public string UsuarioCierre { get; set; }
        public string UsuarioCreacion { get; set; }
        public string DescripcionTipoExplotacion { get; set; }
        public string DescripcionSubTipoExplotacion { get; set; }
        public int? IdSubTipoExplotacion { get; set; }
        public IEnumerable<BE_TxVacunacionSubCutaneaDetalle> ListarTxVacunacionSubCutaneaDetalle { get; set; }
        public IEnumerable<BE_TxVacunacionSubCutaneaFotos> ListarTxVacunacionSubCutaneaFotos { get; set; }
        public IEnumerable<BE_TxVacunacionSubCutaneaIrregularidad> ListarTxVacunacionSubCutaneaIrregularidad { get; set; }
        public IEnumerable<BE_TxVacunacionSubCutaneaMaquina> ListarTxVacunacionSubCutaneaMaquina { get; set; }
        public IEnumerable<BE_TxVacunacionSubCutaneaVacuna> ListarTxVacunacionSubCutaneaVacuna { get; set; }
        public IEnumerable<BE_TxVacunacionSubCutaneaControlEficiencia> ListarTxVacunacionSubCutaneaControlEficiencia { get; set; }
    }
}
