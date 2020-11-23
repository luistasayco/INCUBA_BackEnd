using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindTxRegistroDocumento
    {
        public int IdSubTipoExplotacion { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public DateTime FecInicio { get; set; }
        public DateTime FecFin { get; set; }

        public BE_TxRegistroDocumento RetornaTxRegistroDocumento()
        {
            return new BE_TxRegistroDocumento
            {
                IdSubTipoExplotacion = this.IdSubTipoExplotacion,
                CodigoEmpresa = this.CodigoEmpresa,
                CodigoPlanta = this.CodigoPlanta,
                FecInicio = this.FecInicio,
                FecFin = this.FecFin
            };
        }
    }
}
