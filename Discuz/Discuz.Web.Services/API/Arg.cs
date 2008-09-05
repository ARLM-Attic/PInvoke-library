using System;
#if NET1
#else
using System.Collections.Generic;
#endif
using System.Text;
using System.Xml.Serialization;

namespace Discuz.Web.Services.API
{
    public class Arg
    {
        public Arg()
        { }
        public Arg(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        [XmlElement("key")]
        public string Key;

        [XmlElement("value")]
        public string Value;
    }
}
