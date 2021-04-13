using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IOrganoDetalleRepository : IRepositoryBase<BE_OrganoDetalle>
    {
        Task<IEnumerable<BE_OrganoDetalle>> GetAll(BE_OrganoDetalle entidad);
        Task<BE_OrganoDetalle> GetById(BE_OrganoDetalle entidad);
        Task<int> Create(BE_OrganoDetalle entidad);
        Task Update(BE_OrganoDetalle entidad);
        Task Delete(BE_OrganoDetalle entidad);
    }
}