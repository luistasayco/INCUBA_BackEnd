using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteOrgano : EntityBase
    {
        public int IdOrgano { get; set; }
        public BE_Organo RetornaOrgano()
        {
            return new BE_Organo
            {
                IdOrgano = this.IdOrgano,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
