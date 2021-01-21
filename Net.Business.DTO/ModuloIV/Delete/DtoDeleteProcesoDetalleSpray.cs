using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteProcesoDetalleSpray: EntityBase
    {
        public int IdProcesoDetalleSpray { get; set; }

        public BE_ProcesoDetalleSpray RetornaProcesoDetalleSpray()
        {
            return new BE_ProcesoDetalleSpray
            {
                IdProcesoDetalleSpray = this.IdProcesoDetalleSpray,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
