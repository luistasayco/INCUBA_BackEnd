using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ITxVacunacionSubCutaneaRepository : IRepositoryBase<BE_TxVacunacionSubCutanea>
    {
        Task<IEnumerable<BE_TxVacunacionSubCutanea>> GetAll(FE_TxVacunacionSubCutanea entidad);
        Task<BE_TxVacunacionSubCutanea> GetById(BE_TxVacunacionSubCutanea entidad);
        Task<int> Create(BE_TxVacunacionSubCutanea entidad);
        Task Update(BE_TxVacunacionSubCutanea entidad);
        Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxVacunacionSubCutanea entidad);
        Task Delete(BE_TxVacunacionSubCutanea entidad);
        Task<MemoryStream> GenerarPDF(BE_TxVacunacionSubCutanea entidad);
    }
}