using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateBoquilla: EntityBase
    {
        public int IdBoquilla { get; set; }
        public string DescripcionBoquilla { get; set; }

        public BE_Boquilla RetornaBoquilla()
        {
            return new BE_Boquilla
            {
                IdBoquilla = this.IdBoquilla,
                DescripcionBoquilla = this.DescripcionBoquilla,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
