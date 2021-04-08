using Net.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.DTO
{
    public class DtoUpdateStatusTxSIMConsolidado: EntityBase
    {
        public int IdSIMConsolidado { get; set; }
        public int IdUsuarioCierre { get; set; }

        public BE_TxSIMConsolidado RetornaTxSIMConsolidado()
        {
            return new BE_TxSIMConsolidado
            {
                IdSIMConsolidado = this.IdSIMConsolidado,
                IdUsuarioCierre = this.IdUsuarioCierre,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
