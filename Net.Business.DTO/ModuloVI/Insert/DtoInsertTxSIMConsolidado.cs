﻿using Net.Business.Entities;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoInsertTxSIMConsolidado: EntityBase
    {
        public int IdSIMConsolidado { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public IEnumerable<BE_TxSIMConsolidadoDetalle> ListaTxSIMConsolidadoDetalle { get; set; }

        public BE_TxSIMConsolidado RetornaTxSIMConsolidado()
        {
            return new BE_TxSIMConsolidado
            {
                IdSIMConsolidado = this.IdSIMConsolidado,
                Descripcion = this.Descripcion,
                Observacion = this.Observacion,
                ListaTxSIMConsolidadoDetalle = this.ListaTxSIMConsolidadoDetalle,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
