using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindTxRegistroEquipo: EntityBase
    {
        public int IdRegistroEquipo { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public string CodigoModelo { get; set; }
        public DateTime FecRegistroInicio { get; set; }
        public DateTime FecRegistroFin { get; set; }
        public BE_TxRegistroEquipo TxRegistroEquipo()
        {
            return new BE_TxRegistroEquipo
            {
                IdRegistroEquipo = this.IdRegistroEquipo,
                CodigoEmpresa = this.CodigoEmpresa,
                CodigoPlanta = this.CodigoPlanta,
                CodigoModelo = this.CodigoModelo,
                FecRegistroInicio = this.FecRegistroInicio,
                FecRegistroFin = this.FecRegistroFin,
                RegUsuario = this.RegUsuario
            };
        }
    }
}
