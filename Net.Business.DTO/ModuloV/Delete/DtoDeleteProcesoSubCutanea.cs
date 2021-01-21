using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteProcesoSubCutanea: EntityBase
    {
        public int IdProcesoSubCutanea { get; set; }
        public BE_ProcesoSubCutanea RetornaProcesoSubCutanea()
        {
            return new BE_ProcesoSubCutanea
            {
                IdProcesoSubCutanea = this.IdProcesoSubCutanea,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
