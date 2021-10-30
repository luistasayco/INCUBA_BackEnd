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
    public class DtoFindGeneral
    {
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public string CodigoModelo { get; set; }
        public BE_General General()
        {
            return new BE_General
            {
                CodigoEmpresa = this.CodigoEmpresa,
                CodigoPlanta = this.CodigoPlanta,
                CodigoModelo = this.CodigoModelo
            };
        }
    }

    [DataContract]
    [Serializable]
    [XmlRoot("Lista")]
    public class DtoFindMasivoGeneral
    {
        [DataMember]
        [XmlElement(ElementName = "ListaEmpresaPlanta", Type = typeof(List<DtoEmpresaPlanta>))]
        public List<DtoEmpresaPlanta> ListaEmpresaPlanta { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ListCodigoModelo", Type = typeof(List<DtoModelo>))]
        public List<DtoModelo> ListCodigoModelo { get; set; }
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

    public class DtoEmpresaPlanta
    {
        [DataMember, XmlAttribute]
        public string CodigoEmpresa { get; set; }
        [DataMember, XmlAttribute]
        public string CodigoPlanta { get; set; }
    }

    public class DtoModelo
    {
        [DataMember, XmlAttribute]
        public string CodigoModelo { get; set; }
    }
}
