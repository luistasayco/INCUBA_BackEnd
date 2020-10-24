using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IMantenimientoPorModeloRepository : IRepositoryBase<BE_MantenimientoPorModelo>
    {
        Task<IEnumerable<BE_MantenimientoPorModelo>> GetAll(BE_MantenimientoPorModelo entidad);
        Task<IEnumerable<BE_MantenimientoPorModelo>> GetAllSeleccionado(BE_MantenimientoPorModelo entidad);
        Task<IEnumerable<BE_MantenimientoPorModelo>> GetAllPorSeleccionar(BE_MantenimientoPorModelo entidad);
        Task<int> Create(BE_MantenimientoPorModelo entidad);
        Task Delete(BE_MantenimientoPorModelo entidad);
    }
}
