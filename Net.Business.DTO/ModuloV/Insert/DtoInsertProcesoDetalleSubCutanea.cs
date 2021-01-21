using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoInsertProcesoDetalleSubCutanea: EntityBase
    {
        public int IdProcesoDetalleSubCutanea { get; set; }
        public int IdProcesoSubCutanea { get; set; }
        public string DescripcionProcesoSubCutanea { get; set; }
        public decimal Valor { get; set; }
        public BE_ProcesoDetalleSubCutanea RetornaProcesoDetalleSubCutanea()
        {
            return new BE_ProcesoDetalleSubCutanea
            {
                IdProcesoDetalleSubCutanea = this.IdProcesoDetalleSubCutanea,
                IdProcesoSubCutanea = this.IdProcesoSubCutanea,
                DescripcionProcesoSubCutanea = this.DescripcionProcesoSubCutanea,
                Valor = this.Valor,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
