using Net.Business.Entities;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoUpdateTxSim: EntityBase
    {
        public int IdSIM { get; set; }
        public IEnumerable<BE_TxSIMFotos> ListaTxSIMFotos { get; set; }

        public BE_TxSIM RetornaTxSIM()
        {
            return new BE_TxSIM
            {
                IdSIM = this.IdSIM,
                ListaTxSIMFotos = this.ListaTxSIMFotos,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
