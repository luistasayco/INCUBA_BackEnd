using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateIrregularidad: EntityBase
    {
        public int IdIrregularidad { get; set; }
        public string DescripcionIrregularidad { get; set; }
        public decimal Valor { get; set; }
        public BE_Irregularidad RetornarIrregularidad()
        {
            return new BE_Irregularidad
            {
                IdIrregularidad = this.IdIrregularidad,
                DescripcionIrregularidad = this.DescripcionIrregularidad,
                Valor = this.Valor,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
