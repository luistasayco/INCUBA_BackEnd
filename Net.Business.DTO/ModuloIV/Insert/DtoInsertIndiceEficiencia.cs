using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoInsertIndiceEficiencia : EntityBase
    {
        public int IdIndiceEficiencia { get; set; }
        public string DescripcionIndiceEficiencia { get; set; }
        public decimal RangoInicial { get; set; }
        public decimal RangoFinal { get; set; }
        public decimal Puntaje { get; set; }

        public BE_IndiceEficiencia RetornarIndiceEficiencia()
        {
            return new BE_IndiceEficiencia
            {
                IdIndiceEficiencia = this.IdIndiceEficiencia,
                DescripcionIndiceEficiencia = this.DescripcionIndiceEficiencia,
                RangoInicial = this.RangoInicial,
                RangoFinal = this.RangoFinal,
                Puntaje = this.Puntaje,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
