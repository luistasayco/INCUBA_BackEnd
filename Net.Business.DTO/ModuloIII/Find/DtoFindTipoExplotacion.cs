﻿using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindTipoExplotacion
    {
        public int? IdTipoExplotacion { get; set; }
        public string DescripcionTipoExplotacion { get; set; }

        public BE_TipoExplotacion RetornaTipoExplotacion()
        {
            return new BE_TipoExplotacion
            {
                IdTipoExplotacion = this.IdTipoExplotacion,
                DescripcionTipoExplotacion = this.DescripcionTipoExplotacion
            };
        }
    }
}
