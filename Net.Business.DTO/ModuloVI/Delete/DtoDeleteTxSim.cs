using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteTxSim: EntityBase
    {
        public int IdSIM { get; set; }
        public BE_TxSIM RetornaTxSIM()
        {
            return new BE_TxSIM
            {
                IdSIM = this.IdSIM,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
