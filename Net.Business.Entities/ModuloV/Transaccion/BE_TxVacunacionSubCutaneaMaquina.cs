namespace Net.Business.Entities
{
    public class BE_TxVacunacionSubCutaneaMaquina
    {
        public int IdVacunacionSubCutaneaMaquina { get; set; }
        public int IdVacunacionSubCutanea { get; set; }
        public int IdAguja { get; set; }
        public int NroMaquinas { get; set; }
        public string CodigoModelo { get; set; }
        public string CodigoEquipo { get; set; }
    }
}
