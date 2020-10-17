using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteCalidad : EntityBase
    {
        public int IdCalidad { get; set; }

        public BE_Calidad Calidad()
        {
            return new BE_Calidad
            {
                IdCalidad = this.IdCalidad,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
