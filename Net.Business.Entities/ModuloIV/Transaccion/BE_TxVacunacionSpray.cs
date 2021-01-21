using System;
using System.Collections.Generic;

namespace Net.Business.Entities
{
    public class BE_TxVacunacionSpray: EntityBase
    {
        public int IdVacunacionSpray { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public string Unidad { get; set; }
        public DateTime FecRegistro { get; set; }
        public DateTime FecHoraRegistro { get; set; }
        public string ResponsableInvetsa { get; set; }
        public string ResponsableIncubadora { get; set; }
        public Boolean FlgHyLine { get; set; }
        public Boolean FlgLohman { get; set; }
        public Boolean FlgRoss { get; set; }
        public Boolean FlgCobb { get; set; }
        public Boolean FlgOtros { get; set; }
        public string ObservacionOtros { get; set; }
        public Boolean FlgLunes { get; set; }
        public Boolean FlgMartes { get; set; }
        public Boolean FlgMiercoles { get; set; }
        public Boolean FlgJueves { get; set; }
        public Boolean FlgViernes { get; set; }
        public Boolean FlgSabado { get; set; }
        public Boolean FlgDomingo { get; set; }
        public string ObservacionInvetsa { get; set; }
        public string ObservacionPlanta { get; set; }
        public decimal PromedioPollos { get; set; }
        public string ResponsablePlanta { get; set; }
        public string FirmaInvetsa { get; set; }
        public string FirmaPlanta { get; set; }
        public Boolean FlgCerrado { get; set; }
        public int IdUsuarioCierre { get; set; }
        public DateTime FecCierre { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string NombreArchivo { get; set; }
        public IEnumerable<BE_TxVacunacionSprayDetalle> ListarTxVacunacionSprayDetalle { get; set; }
        public IEnumerable<BE_TxVacunacionSprayFotos> ListarTxVacunacionSprayFotos { get; set; }
        public IEnumerable<BE_TxVacunacionSprayMaquina> ListarTxVacunacionSprayMaquina { get; set; }
        public IEnumerable<BE_TxVacunacionSprayVacuna> ListarTxVacunacionSprayVacuna { get; set; }
    }
}
