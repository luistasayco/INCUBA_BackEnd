using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoInsertSubTipoExplotacion: EntityBase
    {
        public int IdSubTipoExplotacion { get; set; }
        public int IdTipoExplotacion { get; set; }
        public string DescripcionSubTipoExplotacion { get; set; }
        public string NombreDocumento { get; set; }
        public Boolean FlgRequiereFormato { get; set; }
        public Boolean FlgExisteDigital { get; set; }
        public Boolean FlgParaCliente { get; set; }
        public Boolean FlgParaInvetsa { get; set; }

        public BE_SubTipoExplotacion RetornaSubTipoExplotacion()
        {
            return new BE_SubTipoExplotacion
            {
                IdSubTipoExplotacion = this.IdSubTipoExplotacion,
                IdTipoExplotacion = this.IdTipoExplotacion,
                DescripcionSubTipoExplotacion = this.DescripcionSubTipoExplotacion,
                NombreDocumento = this.NombreDocumento,
                FlgRequiereFormato = this.FlgRequiereFormato,
                FlgExisteDigital = this.FlgExisteDigital,
                FlgParaCliente = this.FlgParaCliente,
                FlgParaInvetsa = this.FlgParaInvetsa,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
