using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindOrgano
    {
        public int? IdOrgano { get; set; }
        public string DescripcionOrgano { get; set; }
        public BE_Organo RetornaOrgano()
        {
            return new BE_Organo
            {
                IdOrgano = this.IdOrgano,
                DescripcionOrgano = this.DescripcionOrgano
            };
        }
    }
}
