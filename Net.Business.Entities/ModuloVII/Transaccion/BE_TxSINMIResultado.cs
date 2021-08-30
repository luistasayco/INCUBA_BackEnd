using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.Entities
{
    public class BE_TxSINMIResultado : EntityBase
    {
        public int IdSINMI { get; set; }
        public string Proceso { get; set; }
        public string ProcesoDetalle { get; set; }
        public decimal Ave1 { get; set; }
        public decimal Ave2 { get; set; }
        public decimal Ave3 { get; set; }
        public decimal Ave4 { get; set; }
        public decimal Ave5 { get; set; }
        public int Edad { get; set; }
        public decimal ScoreFinal { get; set; }
        public decimal IndiceHepatico { get; set; }
        public decimal Total { get; set; }
    }
}
