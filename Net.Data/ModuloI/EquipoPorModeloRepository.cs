using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class EquipoPorModeloRepository : RepositoryBase<BE_EquipoPorModelo>, IEquipoPorModeloRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_SELECCIONADO = DB_ESQUEMA + "INC_GetEquipoSeleccionadoPorModelo";

        public EquipoPorModeloRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_EquipoPorModelo>> GetAllSeleccionado(BE_EquipoPorModelo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET_SELECCIONADO));
        }
    }
}
