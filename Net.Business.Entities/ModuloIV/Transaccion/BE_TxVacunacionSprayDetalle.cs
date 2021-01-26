using System;

namespace Net.Business.Entities
{
    public class BE_TxVacunacionSprayDetalle
    {
        public int IdVacunacionSprayDetalle { get; set; }
        public int IdVacunacionSpray { get; set; }
        public int IdProcesoDetalleSpray { get; set; }
        public string DescripcionProcesoDetalleSpray { get; set; }
        public int IdProcesoSpray { get; set; }
        public string DescripcionProcesoSpray { get; set; }
        public int IdProcesoAgrupador { get; set; }
        public decimal ValorProcesoSpray { get; set; }
        public decimal ValorProcesoDetalleSpray { get; set; }
        public Boolean Valor { get; set; }
    }
}
