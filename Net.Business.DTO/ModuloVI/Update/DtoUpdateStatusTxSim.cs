using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoUpdateStatusTxSim: EntityBase
    {
        public int IdSIM { get; set; }
        public int IdUsuarioCierre { get; set; }
        public DateTime FecCierre { get; set; }

        public BE_TxSIM RetornaTxSIM()
        {
            return new BE_TxSIM
            {
                IdSIM = this.IdSIM,
                IdUsuarioCierre = this.IdUsuarioCierre,
                FecCierre = this.FecCierre,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
