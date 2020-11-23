using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindTipoExplotacionPorUsuario: EntityBase
    {
        public EF_TipoExplotacion RetornaTipoExplotacion()
        {
            return new EF_TipoExplotacion
            {
                RegUsuario = this.RegUsuario
            };
        }
    }
}
