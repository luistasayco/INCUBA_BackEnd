using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindProcesoDetalleSubCutanea
    {
        public int IdProcesoSubCutanea { get; set; }
        public string DescripcionProcesoSubCutanea { get; set; }
        public BE_ProcesoDetalleSubCutanea RetornaProcesoDetalleSubCutanea()
        {
            return new BE_ProcesoDetalleSubCutanea
            {
                IdProcesoSubCutanea = this.IdProcesoSubCutanea,
                DescripcionProcesoSubCutanea = this.DescripcionProcesoSubCutanea
            };
        }
    }
}
