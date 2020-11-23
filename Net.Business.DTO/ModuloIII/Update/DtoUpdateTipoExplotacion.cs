using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateTipoExplotacion: EntityBase
    {
        public int IdTipoExplotacion { get; set; }
        public string DescripcionTipoExplotacion { get; set; }

        public BE_TipoExplotacion RetornaTipoExplotacion()
        {
            return new BE_TipoExplotacion
            {
                IdTipoExplotacion = this.IdTipoExplotacion,
                DescripcionTipoExplotacion = this.DescripcionTipoExplotacion,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
