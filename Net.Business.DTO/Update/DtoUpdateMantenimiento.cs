using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateMantenimiento : EntityBase
    {
        public int IdMantenimiento { get; set; }
        public string Descripcion { get; set; }
        public BE_Mantenimiento Mantenimiento()
        {
            return new BE_Mantenimiento
            {
                IdMantenimiento = this.IdMantenimiento,
                Descripcion = this.Descripcion,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
