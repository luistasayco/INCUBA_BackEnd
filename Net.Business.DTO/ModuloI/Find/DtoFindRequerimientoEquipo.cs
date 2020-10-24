using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindRequerimientoEquipo
    {
        public int IdRequerimientoEquipo { get; set; }
        public string Descripcion { get; set; }
        public BE_RequerimientoEquipo RequerimientoEquipo()
        {
            return new BE_RequerimientoEquipo
            {
                IdRequerimientoEquipo = this.IdRequerimientoEquipo,
                Descripcion = this.Descripcion
            };
        }
    }
}
