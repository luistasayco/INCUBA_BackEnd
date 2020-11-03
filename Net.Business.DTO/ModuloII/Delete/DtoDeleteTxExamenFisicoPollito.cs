using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteTxExamenFisicoPollito: EntityBase
    {
        public int IdExamenFisico { get; set; }

        public BE_TxExamenFisicoPollito RetornaTxExamenFisicoPollito()
        {
            return new BE_TxExamenFisicoPollito
            {
                IdExamenFisico = this.IdExamenFisico,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
