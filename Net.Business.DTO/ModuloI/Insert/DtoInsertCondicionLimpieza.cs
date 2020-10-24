using Net.Business.Entities;
using System.ComponentModel.DataAnnotations;

namespace Net.Business.DTO
{
    public class DtoInsertCondicionLimpieza : EntityBase
    {
        public int IdCondicionLimpieza { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatorio")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "La descripción debe estar entre 4 y 100 caracteres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Orden es obligatorio")]
        public int Orden { get; set; }
        public BE_CondicionLimpieza CondicionLimpieza()
        {
            return new BE_CondicionLimpieza
            {
                IdCondicionLimpieza = this.IdCondicionLimpieza,
                Descripcion = this.Descripcion,
                Orden = this.Orden,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
