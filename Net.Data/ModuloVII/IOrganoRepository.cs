using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IOrganoRepository : IRepositoryBase<BE_Organo>
    {
        Task<IEnumerable<BE_Organo>> GetAll(BE_Organo entidad);
        Task<BE_Organo> GetById(BE_Organo entidad);
        Task<int> Create(BE_Organo entidad);
        Task Update(BE_Organo entidad);
        Task Delete(BE_Organo entidad);
    }
}