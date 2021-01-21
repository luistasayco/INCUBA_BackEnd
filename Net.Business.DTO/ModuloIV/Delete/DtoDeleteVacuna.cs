using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteVacuna: EntityBase
    {
        public int IdVacuna { get; set; }
        public BE_Vacuna RetornaVacuna()
        {
            return new BE_Vacuna
            {
                IdVacuna = this.IdVacuna,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
