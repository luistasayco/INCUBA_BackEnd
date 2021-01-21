using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoInsertVacuna : EntityBase
    {
        public int IdVacuna { get; set; }
        public string DescripcionVacuna { get; set; }
        public BE_Vacuna RetornaVacuna()
        {
            return new BE_Vacuna
            {
                IdVacuna = this.IdVacuna,
                DescripcionVacuna = this.DescripcionVacuna,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
