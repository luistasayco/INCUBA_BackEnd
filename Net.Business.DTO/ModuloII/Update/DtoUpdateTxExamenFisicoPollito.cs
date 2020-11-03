using Net.Business.Entities;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoUpdateTxExamenFisicoPollito: EntityBase
    {
        public int IdExamenFisico { get; set; }
        public IEnumerable<BE_TxExamenFisicoPollitoDetalleFotos> ListDetalleFotos { get; set; }
        public BE_TxExamenFisicoPollito RetornaTxExamenFisicoPollito()
        {
            return new BE_TxExamenFisicoPollito
            {
                IdExamenFisico = this.IdExamenFisico,
                ListDetalleFotos = this.ListDetalleFotos,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
