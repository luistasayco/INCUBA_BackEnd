using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateProcesoDetalleSubCutanea: EntityBase
    {
        public int IdProcesoDetalleSubCutanea { get; set; }
        public string DescripcionProcesoSubCutanea { get; set; }
        public decimal Valor { get; set; }
        public BE_ProcesoDetalleSubCutanea RetornaProcesoDetalleSubCutanea()
        {
            return new BE_ProcesoDetalleSubCutanea
            {
                IdProcesoDetalleSubCutanea = this.IdProcesoDetalleSubCutanea,
                DescripcionProcesoSubCutanea = this.DescripcionProcesoSubCutanea,
                Valor = this.Valor,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
