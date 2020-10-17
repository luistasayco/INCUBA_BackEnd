using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindCalidad
    {
        public int IdCalidad { get; set; }
        public string Descripcion { get; set; }

        public BE_Calidad Calidad()
        {
            return new BE_Calidad
            {
                IdCalidad = this.IdCalidad,
                Descripcion = this.Descripcion
            };
        }
    }
}
