using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateCalidad : EntityBase
    {
        public int IdCalidad { get; set; }
        public string Descripcion { get; set; }
        public int RangoInicial { get; set; }
        public int RangoFinal { get; set; }
        public string Color { get; set; }

        public BE_Calidad Calidad()
        {
            return new BE_Calidad
            {
                IdCalidad = this.IdCalidad,
                Descripcion = this.Descripcion,
                RangoInicial = this.RangoInicial,
                RangoFinal = this.RangoFinal,
                Color = this.Color,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
