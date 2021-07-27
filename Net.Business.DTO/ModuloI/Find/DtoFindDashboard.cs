using System;
using System.Collections.Generic;
using System.Text;
using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindDashboard
    {
        public string DashboardCategory { get; set; }

        public FE_DashboardPorCategoria DashboardPorCategoria() 
        {
            return new FE_DashboardPorCategoria
            {
                DashboardCategory = this.DashboardCategory
            };
        }
    }
}
