using System.Collections.Generic;
using Net.Business.Entities;
using Net.Connection;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IDashboardAuditoriaRepository : IRepositoryBase<BE_DashboardAuditoria>
    {
        Task<IEnumerable<BE_DashboardAuditoria>> GetAll(BE_Xml entidad);
    }
}
