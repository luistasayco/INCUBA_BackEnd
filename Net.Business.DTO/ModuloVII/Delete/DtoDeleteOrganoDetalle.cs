using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteOrganoDetalle : EntityBase
    {
        public int IdOrganoDetalle { get; set; }
        public BE_OrganoDetalle RetornaOrganoDetalle()
        {
            return new BE_OrganoDetalle
            {
                IdOrganoDetalle = this.IdOrganoDetalle,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
