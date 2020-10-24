using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateCondicionLimpieza: EntityBase
    {
        public int IdCondicionLimpieza { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public BE_CondicionLimpieza CondicionLimpieza()
        {
            return new BE_CondicionLimpieza
            {
                IdCondicionLimpieza = this.IdCondicionLimpieza,
                Descripcion = this.Descripcion,
                Orden = this.Orden,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
