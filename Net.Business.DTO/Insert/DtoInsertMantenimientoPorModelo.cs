using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoInsertMantenimientoPorModelo : EntityBase
    {
        public int IdMantenimientoPorModelo { get; set; }
        public string CodigoModelo { get; set; }
        public int IdMantenimiento { get; set; }
        public BE_MantenimientoPorModelo MantenimientoPorModelo()
        {
            return new BE_MantenimientoPorModelo
            {
                IdMantenimientoPorModelo = this.IdMantenimientoPorModelo,
                CodigoModelo = this.CodigoModelo,
                IdMantenimiento = this.IdMantenimiento,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
