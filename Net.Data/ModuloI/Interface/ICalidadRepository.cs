using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ICalidadRepository : IRepositoryBase<BE_Calidad>
    {
        Task<IEnumerable<BE_Calidad>> GetAll(BE_Calidad entidad);
        Task<BE_Calidad> GetById(BE_Calidad entidad);
        Task<int> Create(BE_Calidad entidad);
        Task Update(BE_Calidad entidad);
        Task Delete(BE_Calidad entidad);
    }
}
