using Net.Business.Entities;
using System;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoInsertTxRegistroEquipo : EntityBase
    {
        public int IdRegistroEquipo { get; set; }
        public DateTime? FecRegistro { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public string CodigoModelo { get; set; }
        public string FirmaIncuba { get; set; }
        public string FirmaPlanta { get; set; }
        public Boolean FlgCerrado { get; set; }
        public string ResponsableIncuba { get; set; }
        public string ResponsablePlanta { get; set; }
        public int? IdUsuarioCierre { get; set; }
        public DateTime? FecCierre { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string JefePlanta { get; set; }
        public string ObservacionesInvetsa { get; set; }
        public string ObservacionesPlanta { get; set; }
        public List<BE_TxRegistroEquipoDetalle1> TxRegistroEquipoDetalle1 { get; set; }
        public List<BE_TxRegistroEquipoDetalle2> TxRegistroEquipoDetalle2 { get; set; }
        public List<BE_TxRegistroEquipoDetalle3> TxRegistroEquipoDetalle3 { get; set; }
        public List<BE_TxRegistroEquipoDetalle4> TxRegistroEquipoDetalle4 { get; set; }
        public List<BE_TxRegistroEquipoDetalle5> TxRegistroEquipoDetalle5 { get; set; }
        public List<BE_TxRegistroEquipoDetalle6> TxRegistroEquipoDetalle6 { get; set; }
        public List<BE_TxRegistroEquipoDetalle6> TxRegistroEquipoDetalle6Repuestos { get; set; }
        public List<BE_TxRegistroEquipoDetalle7> TxRegistroEquipoDetalle7 { get; set; }

        public BE_TxRegistroEquipo TxRegistroEquipo()
        {
            return new BE_TxRegistroEquipo
            {
                IdRegistroEquipo = this.IdRegistroEquipo,
                FecRegistro = this.FecRegistro,
                CodigoEmpresa = this.CodigoEmpresa,
                CodigoPlanta = this.CodigoPlanta,
                CodigoModelo = this.CodigoModelo,
                FirmaIncuba = this.FirmaIncuba,
                FirmaPlanta = this.FirmaPlanta,
                FlgCerrado = this.FlgCerrado,
                ResponsableIncuba = this.ResponsableIncuba,
                ResponsablePlanta = this.ResponsablePlanta,
                IdUsuarioCierre = this.IdUsuarioCierre,
                FecCierre = this.FecCierre,
                EmailTo = this.EmailTo,
                EmailFrom =this.EmailFrom,
                JefePlanta = this.JefePlanta,
                ObservacionesInvetsa = this.ObservacionesInvetsa,
                ObservacionesPlanta = this.ObservacionesPlanta,
                TxRegistroEquipoDetalle1 = this.TxRegistroEquipoDetalle1,
                TxRegistroEquipoDetalle2 = this.TxRegistroEquipoDetalle2,
                TxRegistroEquipoDetalle3 = this.TxRegistroEquipoDetalle3,
                TxRegistroEquipoDetalle4 = this.TxRegistroEquipoDetalle4,
                TxRegistroEquipoDetalle5 = this.TxRegistroEquipoDetalle5,
                TxRegistroEquipoDetalle6 = this.TxRegistroEquipoDetalle6,
                TxRegistroEquipoDetalle6Repuestos = this.TxRegistroEquipoDetalle6Repuestos,
                TxRegistroEquipoDetalle7 = this.TxRegistroEquipoDetalle7,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
