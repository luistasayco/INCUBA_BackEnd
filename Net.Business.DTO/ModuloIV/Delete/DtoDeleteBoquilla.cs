using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteBoquilla: EntityBase
    {
        public int IdBoquilla { get; set; }

        public BE_Boquilla RetornaBoquilla()
        {
            return new BE_Boquilla
            {
                IdBoquilla = this.IdBoquilla,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
