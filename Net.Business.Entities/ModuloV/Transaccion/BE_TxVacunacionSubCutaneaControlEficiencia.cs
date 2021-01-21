namespace Net.Business.Entities
{
    public class BE_TxVacunacionSubCutaneaControlEficiencia
    {
        public int IdVacunacionSubCutaneaDetalle { get; set; }
        public int IdVacunacionSubCutanea { get; set; }
        public string NombreVacunador { get; set; }
        public int CantidadInicial { get; set; }
        public int CantidadFinal { get; set; }
        public int VacunadoPorHora { get; set; }
        public int PuntajeProductividad { get; set; }
        public int Controlados { get; set; }
        public int SinVacunar { get; set; }
        public int Heridos { get; set; }
        public int Mojados { get; set; }
        public int MalaPosicion { get; set; }
        public int VacunadoCorrectos { get; set; }
    }
}
