using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoResponseTxExamenFisicoPollito
    {
        public int? IdExamenFisico { get; set; }
        public string DescripcionEmpresa { get; set; }
        public DateTime? FecHoraRegistro { get; set; }
        public decimal? Calificacion { get; set; }
        public string DescripcionCalidad { get; set; }
        public decimal? PesoPromedio { get; set; }
        public string UsuarioCreacion { get; set; }

        public DtoResponseTxExamenFisicoPollito RetornarTxExamenFisicoPollitoResponse(BE_TxExamenFisicoPollito TxExamenFisicoPollito)
        {
           return new DtoResponseTxExamenFisicoPollito() { 
                IdExamenFisico = TxExamenFisicoPollito.IdExamenFisico,
                DescripcionEmpresa = TxExamenFisicoPollito.DescripcionEmpresa,
                FecHoraRegistro = TxExamenFisicoPollito.FecHoraRegistro,
                Calificacion = TxExamenFisicoPollito.Calificacion,
                DescripcionCalidad = TxExamenFisicoPollito.DescripcionCalidad,
                PesoPromedio = TxExamenFisicoPollito.PesoPromedio,
                UsuarioCreacion = TxExamenFisicoPollito.UsuarioCreacion
            };
        }
    }
}
