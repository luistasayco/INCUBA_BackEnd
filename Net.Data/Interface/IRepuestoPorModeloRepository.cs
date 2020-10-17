using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IRepuestoPorModeloRepository : IRepositoryBase<BE_RepuestoPorModelo>
    {
        Task<IEnumerable<BE_RepuestoPorModelo>> GetAll(BE_RepuestoPorModelo entidad);
        Task<IEnumerable<BE_RepuestoPorModelo>> GetAllSeleccionado(BE_RepuestoPorModelo entidad);
        Task<IEnumerable<BE_RepuestoPorModelo>> GetAllPorSeleccionar(BE_RepuestoPorModelo entidad);
        Task<int> Create(BE_RepuestoPorModelo entidad);
        Task Delete(BE_RepuestoPorModelo entidad);
    }
}
