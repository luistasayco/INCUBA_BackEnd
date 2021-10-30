using System.Collections.Generic;
using Net.Business.Entities;
using Net.Connection;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IDashboardSINMIRepository : IRepositoryBase<BE_DashboardSINMI>
    {
        Task<IEnumerable<BE_DashboardSINMI>> GetAll(BE_Xml entidad);
    }
}
