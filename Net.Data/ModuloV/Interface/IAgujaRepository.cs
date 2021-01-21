using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IAgujaRepository : IRepositoryBase<BE_Aguja>
    {
        Task<IEnumerable<BE_Aguja>> GetAll(BE_Aguja entidad);
        Task<BE_Aguja> GetById(BE_Aguja entidad);
        Task<int> Create(BE_Aguja entidad);
        Task Update(BE_Aguja entidad);
        Task Delete(BE_Aguja entidad);
    }
}