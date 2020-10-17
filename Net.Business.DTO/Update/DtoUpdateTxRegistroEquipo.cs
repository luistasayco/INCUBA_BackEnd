using Net.Business.Entities;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoUpdateTxRegistroEquipo: EntityBase
    {
        public int IdRegistroEquipo { get; set; }
        public List<BE_TxRegistroEquipoDetalle7> TxRegistroEquipoDetalle7 { get; set; }

        public BE_TxRegistroEquipo TxRegistroEquipo()
        {
            return new BE_TxRegistroEquipo
            {
                IdRegistroEquipo = this.IdRegistroEquipo,
                TxRegistroEquipoDetalle7 = this.TxRegistroEquipoDetalle7,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
