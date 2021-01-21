using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindProcesoDetalleSpray
    {
        public int IdProcesoDetalleSpray { get; set; }
        public int IdProcesoSpray { get; set; }
        public string DescripcionProcesoSpray { get; set; }

        public BE_ProcesoDetalleSpray RetornaProcesoDetalleSpray()
        {
            return new BE_ProcesoDetalleSpray
            {
                IdProcesoDetalleSpray = this.IdProcesoDetalleSpray,
                IdProcesoSpray = this.IdProcesoSpray,
                DescripcionProcesoSpray = this.DescripcionProcesoSpray
            };
        }
    }
}
