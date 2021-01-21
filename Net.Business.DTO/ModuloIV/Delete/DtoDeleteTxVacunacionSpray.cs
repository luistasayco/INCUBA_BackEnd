using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteTxVacunacionSpray: EntityBase
    {
        public int IdVacunacionSpray { get; set; }
        public BE_TxVacunacionSpray RetornaTxVacunacionSpray()
        {
            return new BE_TxVacunacionSpray
            {
                IdVacunacionSpray = this.IdVacunacionSpray,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
