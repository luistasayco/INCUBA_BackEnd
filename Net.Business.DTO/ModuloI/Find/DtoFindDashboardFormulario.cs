using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindDashboardFormulario
    {
        public int Filtro { get; set; }

        public FE_DashboardFormularioPorFiltro DashboardFormularioPorFiltro() 
        {
            return new FE_DashboardFormularioPorFiltro
            {
                Filtro = this.Filtro
            };
        }
    }
}
