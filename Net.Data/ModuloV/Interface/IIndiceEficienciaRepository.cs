using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IIndiceEficienciaRepository : IRepositoryBase<BE_IndiceEficiencia>
    {
        Task<IEnumerable<BE_IndiceEficiencia>> GetAll(BE_IndiceEficiencia entidad);
        Task<BE_IndiceEficiencia> GetById(BE_IndiceEficiencia entidad);
        Task<int> Create(BE_IndiceEficiencia entidad);
        Task Update(BE_IndiceEficiencia entidad);
        Task Delete(BE_IndiceEficiencia entidad);
    }
}
