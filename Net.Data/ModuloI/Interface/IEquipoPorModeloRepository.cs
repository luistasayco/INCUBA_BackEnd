using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IEquipoPorModeloRepository : IRepositoryBase<BE_EquipoPorModelo>
    {
        Task<IEnumerable<BE_EquipoPorModelo>> GetAllSeleccionado(BE_EquipoPorModelo entidad);
    }
}
