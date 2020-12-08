using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoInsertTxRegistroDocumento: EntityBase
    {
        public int IdDocumento { get; set; }
        public int IdTipoExplotacion { get; set; }
        public string DescripcionTipoExplotacion { get; set; }
        public int IdSubTipoExplotacion { get; set; }
        public string DescripcionSubTipoExplotacion { get; set; }
        public string CodigoEmpresa { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public string DescripcionPlanta { get; set; }
        public string NombreArchivo { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }

        public BE_TxRegistroDocumento RetornaTxRegistroDocumento()
        {
            return new BE_TxRegistroDocumento
            {
                IdDocumento = this.IdDocumento,
                IdTipoExplotacion = this.IdTipoExplotacion,
                DescripcionTipoExplotacion = this.DescripcionTipoExplotacion,
                IdSubTipoExplotacion = this.IdSubTipoExplotacion,
                DescripcionSubTipoExplotacion = this.DescripcionSubTipoExplotacion,
                CodigoEmpresa = this.CodigoEmpresa,
                DescripcionEmpresa = this.DescripcionEmpresa,
                CodigoPlanta = this.CodigoPlanta,
                DescripcionPlanta = this.DescripcionPlanta,
                NombreArchivo = this.NombreArchivo,
                EmailFrom = this.EmailFrom,
                EmailTo = this.EmailTo,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
