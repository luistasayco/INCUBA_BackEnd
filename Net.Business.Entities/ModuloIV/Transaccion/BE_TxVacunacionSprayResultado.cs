namespace Net.Business.Entities
{
    public class BE_TxVacunacionSprayResultado
    {
        public int IdVacunacionSprayDetalle { get; set; }
        public int IdVacunacionSpray { get; set; }
        public int IdProcesoAgrupador { get; set; }
        public string DescripcionProcesoAgrupador { get; set; }
        public decimal ValorEsperado { get; set; }
        public decimal ValorObtenido { get; set; }
        public int NroProcesoAcumulado { get; set; }
    }
}
