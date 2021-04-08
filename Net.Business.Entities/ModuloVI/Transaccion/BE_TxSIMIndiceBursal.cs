namespace Net.Business.Entities
{
    public class BE_TxSIMIndiceBursal: EntityBase
    {
        public int IdSIMIndiceBursal { get; set; }
        public int IdSIM { get; set; }
        public int? Edad { get; set; }
        public int Ave { get; set; }
        public decimal PesoCorporal { get; set; }
        public decimal PesoBursa { get; set; }
        public decimal PesoBazo { get; set; }
        public decimal PesoTimo { get; set; }
        public decimal PesoHigado { get; set; }
        public decimal IndiceBursal { get; set; }
        public decimal IndiceTimico { get; set; }
        public decimal IndiceHepatico { get; set; }
        public decimal Bursometro { get; set; }
        public bool FlgPromedio { get; set; }
    }
}
