using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class PlantaRepository : RepositoryBase<BE_Planta>, IPlantaRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetPlantaPorEmpresa";

        public PlantaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Planta>> GetAll(BE_Planta entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
    }
}
