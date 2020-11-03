using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoInsertProcesoDetalle : EntityBase
    {
        public int IdProceso { get; set; }
        public int IdProcesoDetalle { get; set; }
        public string DescripcionProcesoDetalle { get; set; }
        public decimal Factor { get; set; }
        public int Orden { get; set; }
        public string TipoControl { get; set; }
        public BE_ProcesoDetalle ProcesoDetalle()
        {
            return new BE_ProcesoDetalle
            {
                IdProceso = this.IdProceso,
                IdProcesoDetalle = this.IdProcesoDetalle,
                DescripcionProcesoDetalle = this.DescripcionProcesoDetalle,
                Factor = this.Factor,
                Orden = this.Orden,
                TipoControl = this.TipoControl,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
