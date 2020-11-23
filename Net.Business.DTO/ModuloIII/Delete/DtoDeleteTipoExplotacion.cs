using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteTipoExplotacion: EntityBase
    {
        public int IdTipoExplotacion { get; set; }

        public BE_TipoExplotacion RetornaTipoExplotacion()
        {
            return new BE_TipoExplotacion
            {
                IdTipoExplotacion = this.IdTipoExplotacion,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
