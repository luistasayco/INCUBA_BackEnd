using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateAguja: EntityBase
    {
        public int IdAguja { get; set; }
        public string DescripcionAguja { get; set; }
        public BE_Aguja RetornaAguja()
        {
            return new BE_Aguja
            {
                IdAguja = this.IdAguja,
                DescripcionAguja = this.DescripcionAguja,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
