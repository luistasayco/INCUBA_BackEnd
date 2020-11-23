using Net.Connection.Attributes;
using System;
using System.Data;
using System.IO;

namespace Net.Business.Entities
{
    public class BE_MemoryStream
    {
        public MemoryStream FileMemoryStream { get; set; }
        public string TypeFile { get; set; }
        public string NameFile { get; set; }
    }
}
