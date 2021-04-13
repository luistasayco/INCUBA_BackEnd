using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindOrganoDetalle : EntityBase
    {
        public int IdOrgano { get; set; }
        public string DescripcionOrganoDetalle { get; set; }
        public BE_OrganoDetalle RetornaOrganoDetalle()
        {
            return new BE_OrganoDetalle
            {
                IdOrgano = this.IdOrgano,
                DescripcionOrganoDetalle = this.DescripcionOrganoDetalle,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
