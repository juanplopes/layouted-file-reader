using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Simple;

namespace LayoutedReader.Layouts
{
    [XmlRoot("index")]
    public class FileIndex
    {
        [XmlElement("file")]
        public List<FileMapping> Mappings { get; set; }

        public string LayoutFor(string fileName)
        {
            return GetMappingFor(fileName).Layout;
        }

        private FileMapping GetMappingFor(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            var mapping = Mappings.FirstOrDefault(x =>
                fileName.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase));
            if (mapping == null)
                throw new FileNotFoundException("no mapping for file: {0}".AsFormatFor(fileName));
            return mapping;
        }

        public string DeployFor(string fileName)
        {
            return GetMappingFor(fileName).Deploy;
        }
    }
}
