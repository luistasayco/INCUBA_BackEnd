using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoDeleteSubTipoExplotacion: EntityBase
    {
        public int IdSubTipoExplotacion { get; set; }

        public BE_SubTipoExplotacion RetornaSubTipoExplotacion()
        {
            return new BE_SubTipoExplotacion
            {
                IdSubTipoExplotacion = this.IdSubTipoExplotacion,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
