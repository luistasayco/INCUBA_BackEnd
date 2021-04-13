using Net.Business.Entities;
using System;
using System.Collections.Generic;

namespace Net.Business.DTO
{
    public class DtoInsertTxSINMI : EntityBase
    {
        public int IdSINMI { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public int Edad { get; set; }
        public string MotivoVisita { get; set; }
        public DateTime? FecRegistro { get; set; }
        public DateTime? FecHoraRegistro { get; set; }
        public string ResponsableInvetsa { get; set; }
        public string ResponsableIncubadora { get; set; }
        public string ObservacionInvetsa { get; set; }
        public string ObservacionPlanta { get; set; }
        public bool FlgCerrado { get; set; }
        public int IdUsuarioCierre { get; set; }
        public DateTime? FecCierre { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }

        public IEnumerable<BE_TxSINMIDetalle> ListaTxSINMIDetalle { get; set; }
        public IEnumerable<BE_TxSINMIFotos> ListaTxSINMIFotos { get; set; }

        public BE_TxSINMI RetornaTxSINMI()
        {
            return new BE_TxSINMI
            {
                IdSINMI = this.IdSINMI,
                CodigoEmpresa = this.CodigoEmpresa,
                CodigoPlanta = this.CodigoPlanta,
                Edad = this.Edad,
                MotivoVisita = this.MotivoVisita,
                FecRegistro = this.FecRegistro,
                FecHoraRegistro = this.FecHoraRegistro,
                ResponsableInvetsa = this.ResponsableInvetsa,
                ResponsableIncubadora = this.ResponsableIncubadora,
                ObservacionInvetsa = this.ObservacionInvetsa,
                ObservacionPlanta = this.ObservacionPlanta,
                FlgCerrado = this.FlgCerrado,
                IdUsuarioCierre = this.IdUsuarioCierre,
                FecCierre = this.FecCierre,
                EmailFrom = this.EmailFrom,
                EmailTo = this.EmailTo,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion,
                ListaTxSINMIDetalle = this.ListaTxSINMIDetalle,
                ListaTxSINMIFotos = this.ListaTxSINMIFotos
            };
        }
    }
}