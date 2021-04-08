using Net.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.DTO
{
    public class DtoInsertTxSim : EntityBase
    {
        public int IdSIM { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Zona { get; set; }
        public int Galpon { get; set; }
        public int NroPollos { get; set; }
        public DateTime? FecRegistro { get; set; }
        public DateTime? FecHoraRegistro { get; set; }
        public string ResponsableInvetsa { get; set; }
        public string ResponsableIncubadora { get; set; }
        public int RelacionAFavorBursa { get; set; }
        public int RelacionAFavorBazo { get; set; }
        public int Relacion1a1 { get; set; }
        public int AparienciaNormal { get; set; }
        public int AparienciaAnormal { get; set; }
        public int TamanoNormal { get; set; }
        public int TamanoAnormal { get; set; }
        public string ObservacionInvetsa { get; set; }
        public string ObservacionPlanta { get; set; }
        public bool FlgCerrado { get; set; }
        public int IdUsuarioCierre { get; set; }
        public DateTime? FecCierre { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }

        public IEnumerable<BE_TxSIMDigestivo> ListaTxSIMDigestivos { get; set; }
        public IEnumerable<BE_TxSIMFotos> ListaTxSIMFotos { get; set; }
        public IEnumerable<BE_TxSIMIndiceBursal> ListaTxSIMIndiceBursal { get; set; }
        public IEnumerable<BE_TxSIMLesionBursa> ListaTxSIMLesionBursa { get; set; }
        public IEnumerable<BE_TxSIMLesiones> ListaTxSIMLesiones { get; set; }
        public IEnumerable<BE_TxSIMLesionTimo> ListaTxSIMLesionTimo { get; set; }
        public IEnumerable<BE_TxSIMRespiratorio> ListaTxSIMRespiratorio { get; set; }

        public BE_TxSIM RetornaTxSIM()
        {
            return new BE_TxSIM
            {
                IdSIM = this.IdSIM,
                CodigoEmpresa = this.CodigoEmpresa,
                CodigoPlanta = this.CodigoPlanta,
                Edad = this.Edad,
                Sexo = this.Sexo,
                Zona = this.Zona,
                Galpon = this.Galpon,
                NroPollos = this.NroPollos,
                FecRegistro = this.FecRegistro,
                FecHoraRegistro = this.FecHoraRegistro,
                ResponsableInvetsa = this.ResponsableInvetsa,
                ResponsableIncubadora = this.ResponsableIncubadora,
                RelacionAFavorBursa = this.RelacionAFavorBursa,
                RelacionAFavorBazo = this.RelacionAFavorBazo,
                Relacion1a1 = this.Relacion1a1,
                AparienciaNormal = this.AparienciaNormal,
                AparienciaAnormal = this.AparienciaAnormal,
                TamanoNormal = this.TamanoNormal,
                TamanoAnormal = this.TamanoAnormal,
                ObservacionInvetsa = this.ObservacionInvetsa,
                ObservacionPlanta = this.ObservacionPlanta,
                FlgCerrado = this.FlgCerrado,
                IdUsuarioCierre = this.IdUsuarioCierre,
                FecCierre = this.FecCierre,
                EmailFrom = this.EmailFrom,
                EmailTo = this.EmailTo,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion,
                ListaTxSIMDigestivos = this.ListaTxSIMDigestivos,
                ListaTxSIMFotos = this.ListaTxSIMFotos,
                ListaTxSIMIndiceBursal = this.ListaTxSIMIndiceBursal,
                ListaTxSIMLesionBursa = this.ListaTxSIMLesionBursa,
                ListaTxSIMLesiones = this.ListaTxSIMLesiones,
                ListaTxSIMLesionTimo = this.ListaTxSIMLesionTimo,
                ListaTxSIMRespiratorio = this.ListaTxSIMRespiratorio,
            };
        }
    }
}
