using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cetip.FileReader
{
    public class FileMapping
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("layout")]
        public string Layout { get; set; }

        public FileMapping() { }
        public FileMapping(string file, string layout)
        {
            this.Name = file;
            this.Layout = layout;
        }
    }
}
