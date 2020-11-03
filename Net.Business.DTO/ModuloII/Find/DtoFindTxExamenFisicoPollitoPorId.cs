using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindTxExamenFisicoPollitoPorId
    {
        public int IdExamenFisico { get; set; }

        public BE_TxExamenFisicoPollito RetornaTxExamenFisicoPollito()
        {
            return new BE_TxExamenFisicoPollito
            {
                IdExamenFisico = this.IdExamenFisico
            };
        }
    }
}
