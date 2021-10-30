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
    public class DtoFindRepuestoPorModelo
    {
        public string CodigoModelo { get; set; }
        public BE_RepuestoPorModelo RepuestoPorModelo()
        {
            return new BE_RepuestoPorModelo
            {
                CodigoModelo = this.CodigoModelo
            };
        }
    }

    [DataContract]
    [Serializable]
    [XmlRoot("Lista")]
    public class DtoXmlRepuestoPorModelo
    {
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
}
