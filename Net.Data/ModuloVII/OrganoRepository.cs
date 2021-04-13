﻿using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class OrganoRepository : RepositoryBase<BE_Organo>, IOrganoRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_ID = "";
        const string SP_GET = DB_ESQUEMA + "INC_GetOrganoAll";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetOrganoInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetOrganoDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetOrganoUpdate";

        public OrganoRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Organo>> GetAll(BE_Organo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }

        public Task<BE_Organo> GetById(BE_Organo entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_Organo entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_Organo entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_Organo entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
