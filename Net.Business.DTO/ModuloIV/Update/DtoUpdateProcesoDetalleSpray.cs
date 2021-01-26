using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoUpdateProcesoDetalleSpray: EntityBase
    {
        public int IdProcesoDetalleSpray { get; set; }
        public string DescripcionProcesoSpray { get; set; }
        public decimal Valor { get; set; }

        public BE_ProcesoDetalleSpray RetornaProcesoDetalleSpray()
        {
            return new BE_ProcesoDetalleSpray
            {
                IdProcesoDetalleSpray = this.IdProcesoDetalleSpray,
                DescripcionProcesoSpray = this.DescripcionProcesoSpray,
                Valor = this.Valor,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
