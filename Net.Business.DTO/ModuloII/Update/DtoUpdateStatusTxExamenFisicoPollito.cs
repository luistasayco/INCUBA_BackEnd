using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateStatusTxExamenFisicoPollito: EntityBase
    {
        public int IdExamenFisico { get; set; }
        public int IdUsuarioCierre { get; set; }
        public BE_TxExamenFisicoPollito RetornaTxExamenFisicoPollito()
        {
            return new BE_TxExamenFisicoPollito
            {
                IdExamenFisico = this.IdExamenFisico,
                IdUsuarioCierre = this.IdUsuarioCierre,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
