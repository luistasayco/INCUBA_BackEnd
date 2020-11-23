using Net.Business.Entities;
namespace Net.Business.DTO
{
    public class DtoFindSubTipoExplotacionPorUsuario: EntityBase
    {
        public int IdTipoExplotacion { get; set; }
        public EF_SubTipoExplotacion RetornaSubTipoExplotacion()
        {
            return new EF_SubTipoExplotacion
            {
                IdTipoExplotacion = this.IdTipoExplotacion,
                RegUsuario = this.RegUsuario
            };
        }
    }
}
