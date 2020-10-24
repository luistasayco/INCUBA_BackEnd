using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteRequerimientoEquipo: EntityBase
    {
        public int IdRequerimientoEquipo { get; set; }
        public BE_RequerimientoEquipo RequerimientoEquipo()
        {
            return new BE_RequerimientoEquipo
            {
                IdRequerimientoEquipo = this.IdRequerimientoEquipo,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
