namespace Net.Business.Entities
{
    public class BE_TxVacunacionSubCutaneaDetalle
    {
        public int IdVacunacionSubCutaneaDetalle { get; set; }
        public int IdVacunacionSubCutanea { get; set; }
        public int IdProcesoDetalleSubCutanea { get; set; }
        public string Valor { get; set; }
    }
}
