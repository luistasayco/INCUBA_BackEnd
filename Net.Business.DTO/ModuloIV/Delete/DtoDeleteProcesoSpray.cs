using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteProcesoSpray: EntityBase
    {
        public int IdProcesoSpray { get; set; }

        public BE_ProcesoSpray RetornaProcesoSpray()
        {
            return new BE_ProcesoSpray
            {
                IdProcesoSpray = this.IdProcesoSpray,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
