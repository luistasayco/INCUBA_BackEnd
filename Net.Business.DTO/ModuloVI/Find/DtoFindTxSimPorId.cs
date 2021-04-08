using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindTxSimPorId : EntityBase
    {
        public int IdSIM { get; set; }

        public BE_TxSIM RetornaTxSIM()
        {
            return new BE_TxSIM
            {
                IdSIM = this.IdSIM
            };
        }
    }
}
