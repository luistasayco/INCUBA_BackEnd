using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindDashboardSINMI
    {
        public string Empresa { get; set; }
        public string Planta { get; set; }
        public int ResponsableInvetsa { get; set; }
        public string ResponsableCompañia { get; set; }
        public int TipoModulo { get; set; }
        public string Linea { get; set; }
        public int Edad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdDashboard { get; set; }
        public int IdUsuario { get; set; }

        public FE_DashboardSINMIPorFiltro DashboardSINMIPorFiltro() 
        {
            return new FE_DashboardSINMIPorFiltro
            {
                Empresa = this.Empresa,
                Planta = this.Planta,
                ResponsableInvetsa = this.ResponsableInvetsa,
                ResponsableCompañia = this.ResponsableCompañia,
                TipoModulo = this.TipoModulo,
                Linea = this.Linea,
                Edad = this.Edad,
                FechaInicio = this.FechaInicio,
                FechaFin = this.FechaFin,
                IdDashboard = this.IdDashboard,
                IdUsuario = this.IdUsuario
            };
        }
    }
}
