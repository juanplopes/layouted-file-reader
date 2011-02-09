using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LayoutedReader.Layouts
{
    public class FileMapping
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("layout")]
        public string Layout { get; set; }

        [XmlAttribute("deploy")]
        public string Deploy { get; set; }
    }
}
