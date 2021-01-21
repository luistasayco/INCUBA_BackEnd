using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteIrregularidad: EntityBase
    {
        public int IdIrregularidad { get; set; }
        public BE_Irregularidad RetornarIrregularidad()
        {
            return new BE_Irregularidad
            {
                IdIrregularidad = this.IdIrregularidad,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
