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
    public class DtoFindDashboardAuditoria
    {
        [DataMember]
        [XmlElement(ElementName = "ListPlanta", Type = typeof(List<DtoEmpresaPlanta>))]
        public List<DtoEmpresaPlanta> ListPlanta { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListResponsableInvetsa", Type = typeof(List<DtoResponsableInvetsa>))]
        public List<DtoResponsableInvetsa> ListResponsableInvetsa { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListResponsablePlanta", Type = typeof(List<DtoResponsablePlanta>))]
        public List<DtoResponsablePlanta> ListResponsablePlanta { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListLineaGenetica", Type = typeof(List<DtoLineaGenetica>))]
        public List<DtoLineaGenetica> ListLineaGenetica { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListVacunador", Type = typeof(List<DtoVacunador>))]
        public List<DtoVacunador> ListVacunador { get; set; }
        [DataMember, XmlAttribute]
        public int Tipo { get; set; }
        [DataMember, XmlAttribute]
        public DateTime FechaInicio { get; set; }
        [DataMember, XmlAttribute]
        public DateTime FechaFin { get; set; }
        [DataMember, XmlAttribute]
        public int IdDashboard { get; set; }
        [DataMember, XmlAttribute]
        public int IdUsuario { get; set; }

        public BE_Xml General()
        {
            var entiDom = new BE_Xml();
            var ser = new Serializador();
            var ms = new MemoryStream();
            ser.SerializarXml(this, ms);
            entiDom.XmlData = Encoding.UTF8.GetString(ms.ToArray());
            ms.Dispose();

            return new BE_Xml
            {
                XmlData = entiDom.XmlData
            };
        }
    }

    public class DtoVacunador
    {
        [DataMember, XmlAttribute]
        public string Vacunador { get; set; }
    }

    public class DtoLineaGenetica
    {
        [DataMember, XmlAttribute]
        public int LineaGenetica { get; set; }
    }

    public class DtoResponsableInvetsa
    {
        [DataMember, XmlAttribute]
        public int ResponsableInvetsa { get; set; }
    }

    public class DtoResponsablePlanta
    {
        [DataMember, XmlAttribute]
        public string ResponsablePlanta { get; set; }
    }
}
