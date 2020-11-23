using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoInsertProcesoDetalle : EntityBase
    {
        public int IdProceso { get; set; }
        public int IdProcesoDetalle { get; set; }
        public string DescripcionProcesoDetalle { get; set; }
        public Boolean FlgDefault { get; set; }
        public int Orden { get; set; }
        public string TipoControl { get; set; }
        public BE_ProcesoDetalle ProcesoDetalle()
        {
            return new BE_ProcesoDetalle
            {
                IdProceso = this.IdProceso,
                IdProcesoDetalle = this.IdProcesoDetalle,
                DescripcionProcesoDetalle = this.DescripcionProcesoDetalle,
                FlgDefault = this.FlgDefault,
                Orden = this.Orden,
                TipoControl = this.TipoControl,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
