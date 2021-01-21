using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteAguja: EntityBase
    {
        public int IdAguja { get; set; }
        public BE_Aguja RetornaAguja()
        {
            return new BE_Aguja
            {
                IdAguja = this.IdAguja,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
