using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindMantenimientoPorModelo
    {
        public string CodigoModelo { get; set; }
        public BE_MantenimientoPorModelo MantenimientoPorModelo()
        {
            return new BE_MantenimientoPorModelo
            {
                CodigoModelo = this.CodigoModelo
     
            };
        }
    }
}
