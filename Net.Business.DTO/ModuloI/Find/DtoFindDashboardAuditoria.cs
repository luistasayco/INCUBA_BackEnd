using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindDashboardAuditoria
    {
        public string Empresa { get; set; }
        public string Planta { get; set; }
        public int ResponsableInvetsa { get; set; }
        public string ResponsablePlanta { get; set; }
        public int Tipo { get; set; }
        public int LineaGenetica { get; set; }
        public string Vacunador { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdDashboard { get; set; }
        public int IdUsuario { get; set; }

        public FE_DashboardAuditoriaPorFiltro DashboardAuditoriaPorFiltro()
        {
            return new FE_DashboardAuditoriaPorFiltro
            {
                Empresa = this.Empresa,
                Planta = this.Planta,
                ResponsableInvetsa = this.ResponsableInvetsa,
                ResponsablePlanta = this.ResponsablePlanta,
                Tipo = this.Tipo,
                LineaGenetica = this.LineaGenetica,
                Vacunador = this.Vacunador,
                FechaInicio = this.FechaInicio,
                FechaFin = this.FechaFin,
                IdDashboard = this.IdDashboard,
                IdUsuario = this.IdUsuario
            };
        }
    }
}
