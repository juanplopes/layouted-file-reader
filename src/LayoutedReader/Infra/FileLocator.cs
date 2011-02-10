using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LayoutedReader.Infra
{
    public class FileLocator : IFileLocator
    {
        string index;
        public FileLocator(string index)
        {
            this.index = Path.GetFullPath(index);
        }

        public Stream Open(string file)
        {
            return File.OpenRead(Path.Combine(Path.GetDirectoryName(index), file));
        }

        public Stream OpenIndex()
        {
            return File.OpenRead(index);
        }
    }
}
