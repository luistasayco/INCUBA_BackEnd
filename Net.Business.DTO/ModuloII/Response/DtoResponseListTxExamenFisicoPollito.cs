using Net.Business.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Net.Business.DTO
{
    public class DtoResponseListTxExamenFisicoPollito
    {
        public List<DtoResponseTxExamenFisicoPollito> ListaTxExamenFisicoPollito { get; set; }

        public DtoResponseListTxExamenFisicoPollito RetornarTxExamenFisicoPollitoResponse(IEnumerable<BE_TxExamenFisicoPollito> TxExamenFisicoPollito)
        {
            List<DtoResponseTxExamenFisicoPollito> lista = (
                from x in TxExamenFisicoPollito
                select new DtoResponseTxExamenFisicoPollito
            { IdExamenFisico = x.IdExamenFisico,
                DescripcionEmpresa = x.DescripcionEmpresa,
                FecHoraRegistro = x.FecHoraRegistro,
                Calificacion = x.Calificacion,
                DescripcionCalidad = x.DescripcionCalidad,
                PesoPromedio = x.PesoPromedio,
                UsuarioCreacion = x.UsuarioCreacion
            }).ToList();

            return new DtoResponseListTxExamenFisicoPollito() { ListaTxExamenFisicoPollito = lista };
        }
    }
}
