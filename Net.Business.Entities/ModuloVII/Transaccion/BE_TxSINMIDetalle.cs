namespace Net.Business.Entities
{
    public class BE_TxSINMIDetalle: EntityBase
    {
        public int IdSINMIDetalle { get; set; }
        public int IdSINMI { get; set; }
        public int IdOrganoDetalle { get; set; }
        public int IdOrgano { get; set; }
        public int Edad { get; set; }
        public string DescripcionOrgano { get; set; }
        public int Orden { get; set; }
        public string DescripcionOrganoDetalle { get; set; }
        public string Score { get; set; }
        public int OrdenDetalle { get; set; }
        public int TotalDetalle { get; set; }
        public int TotalDocumento { get; set; }
        public decimal Ave1 { get; set; }
        public decimal Ave2 { get; set; }
        public decimal Ave3 { get; set; }
        public decimal Ave4 { get; set; }
        public decimal Ave5 { get; set; }
        public int FactorImpacto { get; set; }
        public bool FlgMedia { get; set; }
        public int ScoreInicio { get; set; }
        public int ScoreFin { get; set; }
    }
}
