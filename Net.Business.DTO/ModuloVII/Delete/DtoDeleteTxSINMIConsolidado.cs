using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteTxSINMIConsolidado : EntityBase
    {
        public int IdSINMIConsolidado { get; set; }

        public BE_TxSINMIConsolidado RetornaTxSINMIConsolidado()
        {
            return new BE_TxSINMIConsolidado
            {
                IdSINMIConsolidado = this.IdSINMIConsolidado,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}