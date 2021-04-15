using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateOrganoDetalle : EntityBase
    {
        public int IdOrganoDetalle { get; set; }
        public string DescripcionOrganoDetalle { get; set; }
        public string Score { get; set; }
        public int ScoreInicio { get; set; }
        public int ScoreFin { get; set; }
        public int OrdenDetalle { get; set; }
        public int FactorImpacto { get; set; }
        public bool FlgMedia { get; set; }
        public BE_OrganoDetalle RetornaOrganoDetalle()
        {
            return new BE_OrganoDetalle
            {
                IdOrganoDetalle = this.IdOrganoDetalle,
                DescripcionOrganoDetalle = this.DescripcionOrganoDetalle,
                Score = this.Score,
                ScoreInicio = this.ScoreInicio,
                ScoreFin = this.ScoreFin,
                OrdenDetalle = this.OrdenDetalle,
                FactorImpacto = this.FactorImpacto,
                FlgMedia = this.FlgMedia,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
