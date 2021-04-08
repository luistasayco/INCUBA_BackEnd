namespace Net.Business.Entities
{
    public class BE_TxSIMRespiratorio: EntityBase
    {
        public int IdSIMRespiratorio { get; set; }
        public int IdSIM { get; set; }
        public int Ave { get; set; }
        public decimal SacosAereos { get; set; }
        public decimal Cornetes { get; set; }
        public decimal Glotis { get; set; }
        public decimal Traquea { get; set; }
        public decimal Pulmones { get; set; }
        public decimal Rinones { get; set; }
        public decimal PlacaPeyer { get; set; }
        public bool FlgGradoLesion { get; set; }
    }
}
