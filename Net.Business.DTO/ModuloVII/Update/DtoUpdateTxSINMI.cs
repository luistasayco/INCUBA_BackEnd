using Net.Business.Entities;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoUpdateTxSINMI : EntityBase
    {
        public int IdSINMI { get; set; }
        public IEnumerable<BE_TxSINMIFotos> ListaTxSINMIFotos { get; set; }

        public BE_TxSINMI RetornaTxSINMI()
        {
            return new BE_TxSINMI
            {
                IdSINMI = this.IdSINMI,
                ListaTxSINMIFotos = this.ListaTxSINMIFotos,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}