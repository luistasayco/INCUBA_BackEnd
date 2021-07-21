using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindDashboardMantenimiento
    {
        /// <summary>
        /// Fecha de Inicio
        /// </summary>
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdDashboard { get; set; }
        public int Tecnico { get; set; }
        public string Empresa { get; set; }
        public string Modelo { get; set; }
        public string Equipo { get; set; }

        public FE_DashboardMantenimientoPorFiltro DashboardMantenimientoPorFiltro() 
        {
            return new FE_DashboardMantenimientoPorFiltro
            {
                FechaInicio = this.FechaInicio,
                FechaFin = this.FechaFin,
                IdDashboard = this.IdDashboard,
                Tecnico = this.Tecnico,
                Empresa = this.Empresa,
                Modelo = this.Modelo,
                Equipo = this.Equipo
            };
        } 
        
    }
}
