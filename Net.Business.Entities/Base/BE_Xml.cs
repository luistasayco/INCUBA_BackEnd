﻿using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Xml
    {
        [DBParameter(SqlDbType.Xml, 0, ActionType.Everything)]
        public string XmlData { get; set; }
    }
}
