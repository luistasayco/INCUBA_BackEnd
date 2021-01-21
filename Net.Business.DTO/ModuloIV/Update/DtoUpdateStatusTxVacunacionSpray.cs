using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateStatusTxVacunacionSpray: EntityBase
    {
        public int IdVacunacionSpray { get; set; }
        public int IdUsuarioCierre { get; set; }

        public BE_TxVacunacionSpray RetornaTxVacunacionSpray()
        {
            return new BE_TxVacunacionSpray
            {
                IdVacunacionSpray = this.IdVacunacionSpray,
                IdUsuarioCierre = this.IdUsuarioCierre,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
