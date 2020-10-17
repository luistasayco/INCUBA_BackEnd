using Net.Business.Entities;
using System.ComponentModel.DataAnnotations;

namespace Net.Business.DTO
{
    public class DtoInsertCalidad : EntityBase
    {
        public int IdCalidad { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatorio")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "La descripción debe estar entre 4 y 100 caracteres")]
        public string Descripcion { get; set; }
        [Required]
        public int RangoInicial { get; set; }
        [Required]
        public int RangoFinal { get; set; }
        [Required(ErrorMessage = "El color es obligatorio")]
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
