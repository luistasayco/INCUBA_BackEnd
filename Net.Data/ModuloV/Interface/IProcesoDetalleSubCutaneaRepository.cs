using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IProcesoDetalleSubCutaneaRepository : IRepositoryBase<BE_ProcesoDetalleSubCutanea>
    {
        Task<IEnumerable<BE_ProcesoDetalleSubCutanea>> GetAll(BE_ProcesoDetalleSubCutanea entidad);
        Task<BE_ProcesoDetalleSubCutanea> GetById(BE_ProcesoDetalleSubCutanea entidad);
        Task<int> Create(BE_ProcesoDetalleSubCutanea entidad);
        Task Update(BE_ProcesoDetalleSubCutanea entidad);
        Task Delete(BE_ProcesoDetalleSubCutanea entidad);
    }
}