using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteProcesoDetalleSubCutanea: EntityBase
    {
        public int IdProcesoDetalleSubCutanea { get; set; }
        public BE_ProcesoDetalleSubCutanea RetornaProcesoDetalleSubCutanea()
        {
            return new BE_ProcesoDetalleSubCutanea
            {
                IdProcesoDetalleSubCutanea = this.IdProcesoDetalleSubCutanea,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
