using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IIrregularidadRepository : IRepositoryBase<BE_Irregularidad>
    {
        Task<IEnumerable<BE_Irregularidad>> GetAll(BE_Irregularidad entidad);
        Task<BE_Irregularidad> GetById(BE_Irregularidad entidad);
        Task<int> Create(BE_Irregularidad entidad);
        Task Update(BE_Irregularidad entidad);
        Task Delete(BE_Irregularidad entidad);
    }
}