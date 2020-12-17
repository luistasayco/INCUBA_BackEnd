using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindTxRegistroDocumentoIdDocumento
    {
        public int IdDocumento { get; set; }

        public BE_TxRegistroDocumento RetornaTxRegistroDocumento()
        {
            return new BE_TxRegistroDocumento
            {
                IdDocumento = this.IdDocumento
            };
        }
    }
}
