using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteCondicionLimpieza : EntityBase
    {
        public int IdCondicionLimpieza { get; set; }
        public BE_CondicionLimpieza CondicionLimpieza()
        {
            return new BE_CondicionLimpieza
            {
                IdCondicionLimpieza = this.IdCondicionLimpieza,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
