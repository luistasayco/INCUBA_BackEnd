using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindEquipo
    {
        public string CodigoEquipo { get; set; }
        public string Descripcion { get; set; }
        public BE_Equipo Equipo()
        {
            return new BE_Equipo
            {
                CodigoEquipo = this.CodigoEquipo,
                Descripcion = this.Descripcion
            };
        }
    }
}
