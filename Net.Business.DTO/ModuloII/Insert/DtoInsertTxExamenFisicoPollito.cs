using Net.Business.Entities;
using System;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoInsertTxExamenFisicoPollito: EntityBase
    {
        public int IdExamenFisico { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public string DescripcionPlanta { get; set; }
        public DateTime? FecRegistro { get; set; }
        public DateTime? FecHoraRegistro { get; set; }
        public string ResponsableInvetsa { get; set; }
        public string ResponsablePlanta { get; set; }
        public int NumeroNacedora { get; set; }
        public string Lote { get; set; }
        public decimal PesoPromedio { get; set; }
        public string EdadReproductora { get; set; }
        public string Sexo { get; set; }
        public string LineaGenetica { get; set; }
        public decimal Calificacion { get; set; }
        public int IdCalidad { get; set; }
        public int Uniformidad { get; set; }
        public string FirmaInvetsa { get; set; }
        public string FirmaPlanta { get; set; }
        public int? IdUsuarioCierre { get; set; }
        public DateTime? FecCierre { get; set; }
        public Boolean? FlgCerrado { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public IEnumerable<BE_TxExamenFisicoPollitoDetalleNew> ListDetalleNew { get; set; }
        public IEnumerable<BE_TxExamenFisicoPollitoDetalleFotos> ListDetalleFotos { get; set; }
        public IEnumerable<BE_TxExamenFisicoPollitoResumen> ListDetalleResumen { get; set; }
        public BE_TxExamenFisicoPollito RetornaTxExamenFisicoPollito()
        {
            return new BE_TxExamenFisicoPollito
            {
                IdExamenFisico = this.IdExamenFisico,
                CodigoEmpresa = this.CodigoEmpresa,
                CodigoPlanta = this.CodigoPlanta,
                DescripcionPlanta = this.DescripcionPlanta,
                FecRegistro = this.FecRegistro,
                ResponsableInvetsa = this.ResponsableInvetsa,
                ResponsablePlanta = this.ResponsablePlanta,
                NumeroNacedora = this.NumeroNacedora,
                Lote = this.Lote,
                PesoPromedio = this.PesoPromedio,
                EdadReproductora = this.EdadReproductora,
                Sexo = this.Sexo,
                LineaGenetica = this.LineaGenetica,
                Calificacion = this.Calificacion,
                IdCalidad = this.IdCalidad,
                Uniformidad = this.Uniformidad,
                FirmaInvetsa = this.FirmaInvetsa,
                FirmaPlanta = this.FirmaPlanta,
                IdUsuarioCierre = this.IdUsuarioCierre,
                FecCierre = this.FecCierre,
                FlgCerrado = this.FlgCerrado,
                ListDetalleNew = this.ListDetalleNew,
                ListDetalleFotos = this.ListDetalleFotos,
                ListDetalleResumen = this.ListDetalleResumen,
                EmailFrom = this.EmailFrom,
                EmailTo = this.EmailTo,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
