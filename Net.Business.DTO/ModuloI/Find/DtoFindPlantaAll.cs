using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindPlantaAll: EntityBase
    {
        public BE_Planta Planta()
        {
            return new BE_Planta
            {
                RegUsuario = this.RegUsuario
            };
        }
    }
}
