using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteTxRegistroDocumento: EntityBase
    {
        public int IdDocumento { get; set; }
        public string IdGoogleDrive { get; set; }

        public BE_TxRegistroDocumento RetornaTxRegistroDocumento()
        {
            return new BE_TxRegistroDocumento
            {
                IdDocumento = this.IdDocumento,
                IdGoogleDrive = this.IdGoogleDrive,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
