namespace Net.Business.Entities
{
   public class BE_TxSIMLesionTimo: EntityBase
    {
        public int IdSIMLesionesTimo { get; set; }
        public int IdSIM { get; set; }
        public int Edad { get; set; }
        public int Ave { get; set; }
        public decimal Valor { get; set; }
        public bool FlgPromedio { get; set; }
    }
}
