using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IModeloRepository : IRepositoryBase<BE_Modelo>
    {
        Task<IEnumerable<BE_Modelo>> GetAll(BE_Modelo entidad);
    }
}
