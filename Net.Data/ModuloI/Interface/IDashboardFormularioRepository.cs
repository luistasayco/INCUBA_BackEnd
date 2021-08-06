using System.Collections.Generic;
using Net.Business.Entities;
using Net.Connection;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IDashboardFormularioRepository : IRepositoryBase<BE_DashboardFormulario>
    {
        Task<IEnumerable<BE_DashboardFormulario>> GetAll(FE_DashboardFormularioPorFiltro entidad);
    }
}
