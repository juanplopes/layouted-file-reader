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
        string directory;

        public FileLoader(string indexFile) : this(new FileLocator(), indexFile) { }

        public FileLoader(IFileLocator locator, string indexFile)
        {
            indexFile = Path.GetFullPath(indexFile);
            directory = Path.GetDirectoryName(indexFile);
            this.locator = locator;

            using (var file = locator.Open(indexFile))
                this.mappings = (FileIndex)new XmlSerializer(typeof(FileIndex)).Deserialize(file);
        }

        private Layout OpenLayout(string layoutFile)
        {
            if (layoutFile == null) return new Layout();

            using (var layoutStream = locator.Open(Path.Combine(directory, layoutFile)))
                return (Layout)new XmlSerializer(typeof(Layout)).Deserialize(layoutStream);
        }

        private Filter OpenDeploy(string deployFile)
        {
            if (deployFile == null) return new Filter();

            using (var deployStream = locator.Open(Path.Combine(directory, deployFile)))
                return (Filter)new XmlSerializer(typeof(Filter)).Deserialize(deployStream);
        }

        public RecordEnumeration Read(string file)
        {
            return new RecordEnumeration(Path.GetFileName(file), ReadInternal(file));
        }

        private IEnumerable<DeployContext> ReadInternal(string file)
        {
            using (var stream = locator.Open(file))
            {
                var layout = OpenLayout(mappings.LayoutFor(file));
                var deploy = OpenDeploy(mappings.DeployFor(file));

                var filename = Path.GetFileName(file);
                
                var stopwatch = Stopwatch.StartNew();
                int expandedCount = 0;
                foreach (var ctx in layout.Read(stream)) {
                    var expanded = deploy.Evaluate(ctx).ToArray();
                    expandedCount += expanded.Length;
                    yield return new DeployContext(ctx, filename, expanded , stopwatch.Elapsed, expandedCount);
                }
            }

        }

    }
}
