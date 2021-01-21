namespace Net.Business.Entities
{
    public class BE_TxVacunacionSprayDetalle
    {
        public int IdVacunacionSprayDetalle { get; set; }
        public int IdVacunacionSpray { get; set; }
        public int IdProcesoDetalleSpray { get; set; }
        public string Valor { get; set; }
    }
}
