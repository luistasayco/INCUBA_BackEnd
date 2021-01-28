using System;

namespace Net.Business.Entities
{
    public class BE_TxVacunacionSubCutaneaDetalle
    {
        public int? IdVacunacionSubCutaneaDetalle { get; set; }
        public int? IdVacunacionSubCutanea { get; set; }
        public int? IdProcesoDetalleSubCutanea { get; set; }
        public string DescripcionProcesoDetalleSubCutanea { get; set; }
        public int? IdProcesoSubCutanea { get; set; }
        public string DescripcionProcesoSubCutanea { get; set; }
        public Boolean? Valor { get; set; }
        public int? IdProcesoAgrupador { get; set; }
        public decimal? ValorProcesoDetalleSubCutanea { get; set; }
        public decimal? ValorProcesoSubCutanea { get; set; }
    }
}
