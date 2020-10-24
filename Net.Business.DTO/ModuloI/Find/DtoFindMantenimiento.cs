using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindMantenimiento
    {
        public int IdMantenimiento { get; set; }
        public string Descripcion { get; set; }
        public BE_Mantenimiento Mantenimiento()
        {
            return new BE_Mantenimiento
            {
                IdMantenimiento = this.IdMantenimiento,
                Descripcion = this.Descripcion
            };
        }
    }
}
