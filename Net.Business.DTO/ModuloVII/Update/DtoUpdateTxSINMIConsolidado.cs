using Net.Business.Entities;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoUpdateTxSINMIConsolidado : EntityBase
    {
        public int IdSINMIConsolidado { get; set; }
        public string Observacion { get; set; }
        public IEnumerable<BE_TxSINMIConsolidadoDetalle> ListaTxSINMIConsolidadoDetalle { get; set; }

        public BE_TxSINMIConsolidado RetornaTxSINMIConsolidado()
        {
            return new BE_TxSINMIConsolidado
            {
                IdSINMIConsolidado = this.IdSINMIConsolidado,
                Observacion = this.Observacion,
                ListaTxSINMIConsolidadoDetalle = this.ListaTxSINMIConsolidadoDetalle,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}