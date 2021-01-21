namespace Net.Business.Entities
{
    public class BE_TxVacunacionSubCutaneaIrregularidad
    {
        public int IdVacunacionSubCutaneaDetalle { get; set; }
        public int IdVacunacionSubCutanea { get; set; }
        public string NombreVacunador { get; set; }
        public string CodigoEquipo { get; set; }
        public int IdIrregularidad { get; set; }
        public decimal Valor { get; set; }

    }
}
