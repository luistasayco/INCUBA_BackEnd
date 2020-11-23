using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindSubTipoExplotacion
    {
        public int IdTipoExplotacion { get; set; }

        public BE_SubTipoExplotacion RetornaSubTipoExplotacion()
        {
            return new BE_SubTipoExplotacion
            {
                IdTipoExplotacion = this.IdTipoExplotacion
            };
        }
    }
}
