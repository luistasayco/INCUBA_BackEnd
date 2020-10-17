using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IEquipoRepository : IRepositoryBase<BE_Equipo>
    {
        Task<IEnumerable<BE_Equipo>> GetAll(BE_General entidad);
    }
}

