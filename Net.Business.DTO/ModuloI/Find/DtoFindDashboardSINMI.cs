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
    public class DtoFindDashboardSINMI
    {
        [DataMember]
        [XmlElement(ElementName = "ListEmpresa", Type = typeof(List<DtoEmpresa>))]
        public List<DtoEmpresa> ListEmpresa { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListPlanta", Type = typeof(List<DtoPlanta>))]
        public List<DtoPlanta> ListPlanta { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListResponsableInvetsa", Type = typeof(List<DtoResponsableInvetsa>))]
        public List<DtoResponsableInvetsa> ListResponsableInvetsa { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListResponsablePlanta", Type = typeof(List<DtoResponsablePlanta>))]
        public List<DtoResponsablePlanta> ListResponsablePlanta { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListLinea", Type = typeof(List<DtoLinea>))]
        public List<DtoLinea> ListLinea { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListEdad", Type = typeof(List<DtoEdad>))]
        public List<DtoEdad> ListEdad { get; set; }
        [DataMember, XmlAttribute]
        public int TipoModulo { get; set; }
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

    public class DtoEmpresa
    {
        [DataMember, XmlAttribute]
        public string Empresa { get; set; }
    }
    public class DtoPlanta
    {
        [DataMember, XmlAttribute]
        public string Planta { get; set; }
    }
    public class DtoLinea
    {
        [DataMember, XmlAttribute]
        public string Linea { get; set; }
    }
    public class DtoEdad
    {
        [DataMember, XmlAttribute]
        public int Edad { get; set; }
    }
}
