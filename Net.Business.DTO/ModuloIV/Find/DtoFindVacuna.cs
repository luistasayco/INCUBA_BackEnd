using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindVacuna
    {
        public int IdVacuna { get; set; }
        public string DescripcionVacuna { get; set; }
        public BE_Vacuna RetornaVacuna()
        {
            return new BE_Vacuna
            {
                IdVacuna = this.IdVacuna,
                DescripcionVacuna = this.DescripcionVacuna
            };
        }
    }
}
