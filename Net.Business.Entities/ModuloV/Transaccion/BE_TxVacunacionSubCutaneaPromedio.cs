namespace Net.Business.Entities
{
    public class BE_TxVacunacionSubCutaneaPromedio
    {
        public int IdVacunacionSubCutaneaDetalle { get; set; }
        public int IdVacunacionSubCutanea { get; set; }
        public string NombreVacunador { get; set; }
        public decimal VacunadoPorHora { get; set; }
        public decimal PuntajeProductividad { get; set; }
        public decimal Controlados { get; set; }
        public decimal SinVacunar { get; set; }
        public decimal Heridos { get; set; }
        public decimal Mojados { get; set; }
        public decimal MalaPosicion { get; set; }
        public decimal VacunadoCorrectos { get; set; }
        public decimal PorcentajeEficiencia { get; set; }
        public decimal PuntajeEficiencia { get; set; }
    }
}
