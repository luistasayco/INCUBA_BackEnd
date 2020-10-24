using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateRequerimientoEquipo: EntityBase
    {
        public int IdRequerimientoEquipo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public BE_RequerimientoEquipo RequerimientoEquipo()
        {
            return new BE_RequerimientoEquipo
            {
                IdRequerimientoEquipo = this.IdRequerimientoEquipo,
                Descripcion = this.Descripcion,
                Orden = this.Orden,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
