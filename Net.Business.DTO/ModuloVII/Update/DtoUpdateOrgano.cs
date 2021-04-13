using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateOrgano : EntityBase
    {
        public int IdOrgano { get; set; }
        public string DescripcionOrgano { get; set; }
        public int Orden { get; set; }
        public BE_Organo RetornaOrgano()
        {
            return new BE_Organo
            {
                IdOrgano = this.IdOrgano,
                DescripcionOrgano = this.DescripcionOrgano,
                Orden = this.Orden,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
