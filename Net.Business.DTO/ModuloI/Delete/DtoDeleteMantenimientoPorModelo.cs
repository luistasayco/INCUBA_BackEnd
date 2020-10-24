using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteMantenimientoPorModelo : EntityBase
    {
        public string CodigoModelo { get; set; }
        public int IdMantenimiento { get; set; }
        public BE_MantenimientoPorModelo MantenimientoPorModelo()
        {
            return new BE_MantenimientoPorModelo
            {
                CodigoModelo = this.CodigoModelo,
                IdMantenimiento = this.IdMantenimiento,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
