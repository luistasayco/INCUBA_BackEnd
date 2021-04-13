using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateOrganoDetalle : EntityBase
    {
        public int IdOrganoDetalle { get; set; }
        public string DescripcionOrganoDetalle { get; set; }
        public string Score { get; set; }
        public int OrdenDetalle { get; set; }
        public BE_OrganoDetalle RetornaOrganoDetalle()
        {
            return new BE_OrganoDetalle
            {
                IdOrganoDetalle = this.IdOrganoDetalle,
                DescripcionOrganoDetalle = this.DescripcionOrganoDetalle,
                Score = this.Score,
                OrdenDetalle = this.OrdenDetalle,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
