﻿using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class ModeloRepository : RepositoryBase<BE_Modelo>, IModeloRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetModeloAll";

        public ModeloRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Modelo>> GetAll(BE_Modelo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
    }
}
