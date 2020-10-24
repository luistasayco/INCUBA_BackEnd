using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IEmpresaRepository : IRepositoryBase<BE_Empresa>
    {
        Task<IEnumerable<BE_Empresa>> GetAll(BE_Empresa entidad);
    }
}
