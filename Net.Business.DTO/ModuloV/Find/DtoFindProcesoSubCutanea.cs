using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindProcesoSubCutanea: EntityBase
    {
        public int IdProcesoSubCutanea { get; set; }
        public string DescripcionProcesoSubCutanea { get; set; }
        public BE_ProcesoSubCutanea RetornaProcesoSubCutanea()
        {
            return new BE_ProcesoSubCutanea
            {
                IdProcesoSubCutanea = this.IdProcesoSubCutanea,
                DescripcionProcesoSubCutanea = this.DescripcionProcesoSubCutanea,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
