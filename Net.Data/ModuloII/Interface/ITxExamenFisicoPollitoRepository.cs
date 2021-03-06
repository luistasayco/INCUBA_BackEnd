﻿using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ITxExamenFisicoPollitoRepository : IRepositoryBase<BE_TxExamenFisicoPollito>
    {
        Task<IEnumerable<BE_TxExamenFisicoPollito>> GetAll(FE_TxExamenFisicoPollito entidad);
        Task<IEnumerable<BE_TxExamenFisicoPollitoDetalleNew>> GetByDetalleNew(BE_TxExamenFisicoPollito entidad);
        Task<BE_TxExamenFisicoPollito> GetById(BE_TxExamenFisicoPollito entidad);
        Task<BE_TxExamenFisicoPollito> GetByIdNew(BE_TxExamenFisicoPollito entidad);
        Task<BE_ResultadoTransaccion> Create(BE_TxExamenFisicoPollito entidad);
        Task Update(BE_TxExamenFisicoPollito entidad);
        Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxExamenFisicoPollito entidad);
        Task Delete(BE_TxExamenFisicoPollito entidad);
        Task<MemoryStream> GenerarPDF(BE_TxExamenFisicoPollito entidad);
    }
}
