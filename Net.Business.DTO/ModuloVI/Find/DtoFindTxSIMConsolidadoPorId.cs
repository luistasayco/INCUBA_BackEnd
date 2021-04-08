using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindTxSIMConsolidadoPorId
    {
        public int IdSIMConsolidado { get; set; }

        public BE_TxSIMConsolidado RetornaTxSIMConsolidado()
        {
            return new BE_TxSIMConsolidado
            {
                IdSIMConsolidado = this.IdSIMConsolidado
            };
        }
    }
}
