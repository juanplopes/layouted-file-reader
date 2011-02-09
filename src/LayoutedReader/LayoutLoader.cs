using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Abstractions;
using System.Xml.Serialization;
using System.IO;

namespace Cetip.FileReader
{
    public class LayoutLoader
    {
        IFileSystem system;
        string mappingFile;
        FileMappings mappings;
        public FileMappings Mappings { get { return mappings; } }

        public LayoutLoader(IFileSystem system, string fileName)
        {
            mappingFile = fileName;
            this.system = system;
            using (var file = system.File.OpenRead(fileName))
                this.mappings = (FileMappings)new XmlSerializer(typeof(FileMappings)).Deserialize(file);
        }

        public ImportedFile ReadAll(string file)
        {
            using (var layoutStream = OpenLayoutStream(file))
            {
                var layout = GetLayoutFrom(layoutStream);
                using (var stream = system.File.OpenRead(file))
                    return layout.ReadAll(stream);
            }
        }

        private static Layout GetLayoutFrom(Stream layoutStream)
        {
            return (Layout)new XmlSerializer(typeof(Layout)).Deserialize(layoutStream);
        }

        private Stream OpenLayoutStream(string file)
        {
            return system.File.OpenRead(mappings.LayoutFor(file));
        }

        public void ReadEach(string file, Action<RecordContext> action)
        {
            using (var layoutStream = OpenLayoutStream(file))
            {
                var layout = GetLayoutFrom(layoutStream);
                using (var stream = system.File.OpenRead(file))
                    layout.ReadEach(stream, action);
            }
        }

        public void ReadAsync(string file, Action<RecordContext> action)
        {
            using (var layoutStream = OpenLayoutStream(file))
            {
                var layout = GetLayoutFrom(layoutStream);
                using (var stream = system.File.OpenRead(file))
                    layout.ReadAsync(stream, action);
            }
        }
    }
}
