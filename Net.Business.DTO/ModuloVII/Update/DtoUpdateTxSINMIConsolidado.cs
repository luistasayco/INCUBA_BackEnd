using Net.Business.Entities;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoUpdateTxSINMIConsolidado : EntityBase
    {
        public int IdSINMIConsolidado { get; set; }
        public string Conclusion { get; set; }
        public string Resultado { get; set; }
        public string Linea { get; set; }
        public string PersonaContacto { get; set; }
        public IEnumerable<BE_TxSINMIConsolidadoDetalle> ListaTxSINMIConsolidadoDetalle { get; set; }

        public BE_TxSINMIConsolidado RetornaTxSINMIConsolidado()
        {
            return new BE_TxSINMIConsolidado
            {
                IdSINMIConsolidado = this.IdSINMIConsolidado,
                Conclusion = this.Conclusion,
                Resultado = this.Resultado,
                Linea = this.Linea,
                PersonaContacto = this.PersonaContacto,
                ListaTxSINMIConsolidadoDetalle = this.ListaTxSINMIConsolidadoDetalle,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}