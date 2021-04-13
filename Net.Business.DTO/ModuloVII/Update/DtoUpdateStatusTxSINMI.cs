using Net.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.DTO
{
    public class DtoUpdateStatusTxSINMI :  EntityBase
    {
        public int IdSINMI { get; set; }
        public int IdUsuarioCierre { get; set; }
        public DateTime FecCierre { get; set; }

        public BE_TxSINMI RetornaTxSINMI()
        {
            return new BE_TxSINMI
            {
                IdSINMI = this.IdSINMI,
                IdUsuarioCierre = this.IdUsuarioCierre,
                FecCierre = this.FecCierre,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
