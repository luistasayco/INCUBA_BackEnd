using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateStatusTxSINMIConsolidado : EntityBase
    {
        public int IdSINMIConsolidado { get; set; }
        public int IdUsuarioCierre { get; set; }

        public BE_TxSINMIConsolidado RetornaTxSINMIConsolidado()
        {
            return new BE_TxSINMIConsolidado
            {
                IdSINMIConsolidado = this.IdSINMIConsolidado,
                IdUsuarioCierre = this.IdUsuarioCierre,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}