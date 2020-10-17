using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteProcesoDetalle : EntityBase
    {
        public int IdProcesoDetalle { get; set; }
        public BE_ProcesoDetalle ProcesoDetalle()
        {
            return new BE_ProcesoDetalle
            {
                IdProcesoDetalle = this.IdProcesoDetalle,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
