using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IDashboardMantenimientoRepository : IRepositoryBase<BE_DashboardMantenimiento>
    {
        Task<IEnumerable<BE_DashboardMantenimiento>> GetAll(BE_Xml entidad);
    }
}
