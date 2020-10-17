using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteMantenimiento: EntityBase
    {
        public int IdMantenimiento { get; set; }
        public BE_Mantenimiento Mantenimiento()
        {
            return new BE_Mantenimiento
            {
                IdMantenimiento = this.IdMantenimiento,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
