using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IPlantaRepository: IRepositoryBase<BE_Planta>
    {
        Task<IEnumerable<BE_Planta>> GetAll(BE_Planta entidad);
        Task<IEnumerable<BE_Planta>> GetAlls(BE_Planta entidad);
    }
}
