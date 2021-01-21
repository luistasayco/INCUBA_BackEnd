using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindProcesoSpray
    {
        public int IdProcesoSpray { get; set; }
        public string DescripcionProcesoSpray { get; set; }

        public BE_ProcesoSpray RetornaProcesoSpray()
        {
            return new BE_ProcesoSpray
            {
                IdProcesoSpray = this.IdProcesoSpray,
                DescripcionProcesoSpray = this.DescripcionProcesoSpray
            };
        }
    }
}
