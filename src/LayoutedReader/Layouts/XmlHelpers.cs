using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace LayoutedReader.Layouts
{
    public static class XmlHelpers
    {
        public static T AsXmlOf<T>(this Stream stream)
        {
            return (T)new XmlSerializer(typeof(T)).Deserialize(stream);
        }
    }
}
