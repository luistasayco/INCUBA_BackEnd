using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateStatusTxVacunacionSubCutanea: EntityBase
    {
        public int IdVacunacionSubCutanea { get; set; }
        public int IdUsuarioCierre { get; set; }

        public BE_TxVacunacionSubCutanea RetornaTxVacunacionSubCutanea()
        {
            return new BE_TxVacunacionSubCutanea
            {
                IdVacunacionSubCutanea = this.IdVacunacionSubCutanea,
                IdUsuarioCierre = this.IdUsuarioCierre,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
