using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindTxRegistroDocumentoId
    {
        public string IdGoogleDrive { get; set; }

        public BE_TxRegistroDocumento RetornaTxRegistroDocumento()
        {
            return new BE_TxRegistroDocumento
            {
                IdGoogleDrive = this.IdGoogleDrive
            };
        }
    }
}
