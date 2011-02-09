using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.IO.Abstractions;
using Simple;

namespace Cetip.FileReader
{
    [XmlRoot("mappings")]
    public class FileMappings
    {
        [XmlElement("file")]
        public List<FileMapping> Mappings { get; set; }

        public string LayoutFor(string fileName)
        {
            var mapping = Mappings.FirstOrDefault(x =>
                Path.GetFileName(fileName).Equals(x.Name, StringComparison.InvariantCultureIgnoreCase));
            if (mapping == null)
                throw new FileNotFoundException("no mapping for file: {0}".AsFormat(fileName));
            return mapping.Layout;
        }
    }
}
