using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IDashboardRepository : IRepositoryBase<BE_Dashboard>
    {
        Task<IEnumerable<BE_Dashboard>> GetAll(FE_DashboardPorCategoria entidad);
    }
}
