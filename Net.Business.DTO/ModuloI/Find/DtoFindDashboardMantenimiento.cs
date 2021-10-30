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
    public class DtoFindDashboardMantenimiento
    {
        [DataMember, XmlAttribute]
        public DateTime FechaInicio { get; set; }
        [DataMember, XmlAttribute]
        public DateTime FechaFin { get; set; }
        [DataMember, XmlAttribute]
        public int IdDashboard { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListTecnico", Type = typeof(List<DtoTecnico>))]
        public List<DtoTecnico> ListTecnico { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListRespuesto", Type = typeof(List<DtoRespuesto>))]
        public List<DtoRespuesto> ListRespuesto { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListPlanta", Type = typeof(List<DtoEmpresaPlanta>))]
        public List<DtoEmpresaPlanta> ListPlanta { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListModelo", Type = typeof(List<DtoModelo>))]
        public List<DtoModelo> ListModelo { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListEquipo", Type = typeof(List<DtoEquipo>))]
        public List<DtoEquipo> ListEquipo { get; set; }
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

    public class DtoTecnico
    {
        [DataMember, XmlAttribute]
        public int Tecnico { get; set; }
    }

    public class DtoRespuesto
    {
        [DataMember, XmlAttribute]
        public string Respuesto { get; set; }
    }
    //public class DtoPlanta
    //{
    //    [DataMember, XmlAttribute]
    //    public string Planta { get; set; }
    //}
    public class DtoEquipo
    {
        [DataMember, XmlAttribute]
        public string Equipo { get; set; }
    }
}
