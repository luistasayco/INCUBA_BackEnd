using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindCondicionLimpieza
    {
        public int IdCondicionLimpieza { get; set; }
        public string Descripcion { get; set; }

        public BE_CondicionLimpieza CondicionLimpieza()
        {
            return new BE_CondicionLimpieza
            {
                IdCondicionLimpieza = this.IdCondicionLimpieza,
                Descripcion = this.Descripcion
            };
        }
    }
}
