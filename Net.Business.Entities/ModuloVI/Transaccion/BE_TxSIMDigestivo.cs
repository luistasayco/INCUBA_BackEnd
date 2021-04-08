using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Business.Entities
{
    public class BE_TxSIMDigestivo: EntityBase
    {
        public int IdSIMDigestivo { get; set; }
        public int IdSIM { get; set; }
        public int Ave { get; set; }
        public decimal Duademo { get; set; }
        public decimal Yeyuno { get; set; }
        public decimal Lleon { get; set; }
        public decimal Ciegos { get; set; }
        public decimal Tonsillas { get; set; }
        public decimal Higados { get; set; }
        public decimal Molleja { get; set; }
        public decimal Proventriculo { get; set; }
        public bool FlgGradoLesion { get; set; }
    }
}
