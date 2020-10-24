using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteRepuestoPorModelo : EntityBase
    {
        public string CodigoModelo { get; set; }
        public string CodigoRepuesto { get; set; }
        public BE_RepuestoPorModelo RepuestoPorModelo()
        {
            return new BE_RepuestoPorModelo
            {
                CodigoModelo = this.CodigoModelo,
                CodigoRepuesto = this.CodigoRepuesto,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
