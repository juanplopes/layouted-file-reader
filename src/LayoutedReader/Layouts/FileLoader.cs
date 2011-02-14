using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using LayoutedReader.Infra;
using System.IO;
using LayoutedReader.Layouts;
using System.Diagnostics;
using System.Threading;

namespace LayoutedReader.Layouts
{
    public class FileLoader
    {
        IFileLocator locator;
        FileIndex mappings;

        public FileLoader(string indexFile) : this(new FileLocator(indexFile)) { }

        public FileLoader(IFileLocator locator)
        {
            this.locator = locator;
            using (var file = locator.OpenIndex())
                this.mappings = (FileIndex)new XmlSerializer(typeof(FileIndex)).Deserialize(file);
        }

        private T OpenXml<T>(string file)
        {
            if (file == null) return default(T);

            using (var stream = locator.OpenRelative(file))
                return stream.AsXmlOf<T>();
        }


        public FileContext<DeployContext> Read(string file)
        {
            DeployContext header;
            using (var stream = locator.OpenAny(file))
                header = OpenFile(file, stream).HeaderContext;

            return new FileContext<DeployContext>(header, GetEnumerator(file));            
        }

        public IEnumerable<DeployContext> GetEnumerator(string file)
        {
            

            using (var stream = locator.OpenAny(file))
            {
                var items = OpenFile(file, stream);
                foreach (var item in items)
                    yield return item;
            }
        }

        private FileContext<DeployContext> OpenFile(string file, Stream stream)
        {
            var layout = OpenXml<Layout>(mappings.LayoutFor(file));
            var deploy = OpenXml<Filter>(mappings.DeployFor(file));
            var items = new Deployer(layout, deploy).Read(stream);
            return items;
        }

    }
}
