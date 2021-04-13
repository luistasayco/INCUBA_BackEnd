using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindTxSINMIConsolidadoPorId
    {
        public int IdSINMIConsolidado { get; set; }

        public BE_TxSINMIConsolidado RetornaTxSINMIConsolidado()
        {
            return new BE_TxSINMIConsolidado
            {
                IdSINMIConsolidado = this.IdSINMIConsolidado
            };
        }
    }
}
