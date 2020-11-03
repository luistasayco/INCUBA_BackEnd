using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindProcesoDetalle
    { 
        public int IdProceso { get; set; }

        public BE_ProcesoDetalle ProcesoDetalle()
        {
            return new BE_ProcesoDetalle
            {
                IdProceso = this.IdProceso,
            };
        }
    }
}
