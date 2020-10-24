using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindRepuestoPorModelo
    {
        public string CodigoModelo { get; set; }
        public BE_RepuestoPorModelo RepuestoPorModelo()
        {
            return new BE_RepuestoPorModelo
            {
                CodigoModelo = this.CodigoModelo
            };
        }
    }
}
