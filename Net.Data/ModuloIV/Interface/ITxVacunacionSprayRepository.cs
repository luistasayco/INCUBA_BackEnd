using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ITxVacunacionSprayRepository : IRepositoryBase<BE_TxVacunacionSpray>
    {
        Task<IEnumerable<BE_TxVacunacionSpray>> GetAll(FE_TxVacunacionSpray entidad);
        Task<BE_TxVacunacionSpray> GetById(BE_TxVacunacionSpray entidad);
        Task<BE_TxVacunacionSpray> GetByIdNew(BE_TxVacunacionSpray entidad);
        Task<int> Create(BE_TxVacunacionSpray entidad);
        Task Update(BE_TxVacunacionSpray entidad);
        Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxVacunacionSpray entidad);
        Task Delete(BE_TxVacunacionSpray entidad);
        Task<MemoryStream> GenerarPDF(BE_TxVacunacionSpray entidad);
    }
}