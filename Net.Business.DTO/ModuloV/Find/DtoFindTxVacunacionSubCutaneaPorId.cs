using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindTxVacunacionSubCutaneaPorId: EntityBase
    {
        public int IdVacunacionSubCutanea { get; set; }

        public BE_TxVacunacionSubCutanea RetornaTxVacunacionSubCutanea()
        {
            return new BE_TxVacunacionSubCutanea
            {
                IdVacunacionSubCutanea = this.IdVacunacionSubCutanea
            };
        }
    }
}
