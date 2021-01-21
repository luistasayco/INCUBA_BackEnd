using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IBoquillaRepository : IRepositoryBase<BE_Boquilla>
    {
        Task<IEnumerable<BE_Boquilla>> GetAll(BE_Boquilla entidad);
        Task<BE_Boquilla> GetById(BE_Boquilla entidad);
        Task<int> Create(BE_Boquilla entidad);
        Task Update(BE_Boquilla entidad);
        Task Delete(BE_Boquilla entidad);
    }
}
