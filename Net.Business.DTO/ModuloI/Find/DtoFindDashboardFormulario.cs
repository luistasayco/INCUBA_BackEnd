using Net.Business.Entities;
using Net.CrossCotting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Net.Business.DTO
{
    [DataContract]
    [Serializable]
    [XmlRoot("Lista")]
    public class DtoFindDashboardFormulario
    {
        public int Filtro { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListPlanta", Type = typeof(List<DtoEmpresaPlanta>))]
        public List<DtoEmpresaPlanta> ListPlanta { get; set; }


        public FE_DashboardFormularioPorFiltro DashboardFormularioPorFiltro() 
        {

            var entiDom = new BE_Xml();
            var ser = new Serializador();
            var ms = new MemoryStream();
            ser.SerializarXml(this, ms);
            entiDom.XmlData = Encoding.UTF8.GetString(ms.ToArray());
            ms.Dispose();

            return new FE_DashboardFormularioPorFiltro
            {
                Filtro = this.Filtro,
                xmlData = entiDom.XmlData
            };
        }
    }
}
