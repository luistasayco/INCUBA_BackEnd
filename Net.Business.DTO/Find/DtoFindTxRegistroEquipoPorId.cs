using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindTxRegistroEquipoPorId
    {
        public int IdRegistroEquipo { get; set; }
        public BE_TxRegistroEquipo TxRegistroEquipo()
        {
            return new BE_TxRegistroEquipo
            {
                IdRegistroEquipo = this.IdRegistroEquipo
            };
        }
    }
}
