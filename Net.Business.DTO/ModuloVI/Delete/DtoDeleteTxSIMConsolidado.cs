using Net.Business.Entities;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoDeleteTxSIMConsolidado: EntityBase
    {
        public int IdSIMConsolidado { get; set; }

        public BE_TxSIMConsolidado RetornaTxSIMConsolidado()
        {
            return new BE_TxSIMConsolidado
            {
                IdSIMConsolidado = this.IdSIMConsolidado,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
