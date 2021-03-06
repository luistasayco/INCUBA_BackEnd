﻿using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class CondicionLimpiezaRepository : RepositoryBase<BE_CondicionLimpieza>, ICondicionLimpiezaRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetCondicionLimpiezaAll";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetCondicionLimpiezaAll";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetCondicionLimpiezaInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetCondicionLimpiezaDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetCondicionLimpiezaUpdate";

        public CondicionLimpiezaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_CondicionLimpieza>> GetAll(BE_CondicionLimpieza entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_CondicionLimpieza> GetById(BE_CondicionLimpieza entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_CondicionLimpieza entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_CondicionLimpieza entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_CondicionLimpieza entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
