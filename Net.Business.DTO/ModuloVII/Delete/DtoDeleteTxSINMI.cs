using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteTxSINMI : EntityBase
    {
        public int IdSINMI { get; set; }

        public BE_TxSINMI RetornaTxSINMI()
        {
            return new BE_TxSINMI
            {
                IdSINMI = this.IdSINMI,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}