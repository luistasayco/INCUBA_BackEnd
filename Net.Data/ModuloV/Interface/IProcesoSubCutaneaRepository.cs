using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IProcesoSubCutaneaRepository : IRepositoryBase<BE_ProcesoSubCutanea>
    {
        Task<IEnumerable<BE_ProcesoSubCutanea>> GetAll(BE_ProcesoSubCutanea entidad);
        Task<BE_ProcesoSubCutanea> GetById(BE_ProcesoSubCutanea entidad);
        Task<int> Create(BE_ProcesoSubCutanea entidad);
        Task Update(BE_ProcesoSubCutanea entidad);
        Task Delete(BE_ProcesoSubCutanea entidad);
    }
}