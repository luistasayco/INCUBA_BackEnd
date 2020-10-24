using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteTxRegistroEquipo: EntityBase
    {
        public int IdRegistroEquipo { get; set; }

        public BE_TxRegistroEquipo TxRegistroEquipo()
        {
            return new BE_TxRegistroEquipo
            {
                IdRegistroEquipo = this.IdRegistroEquipo,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
