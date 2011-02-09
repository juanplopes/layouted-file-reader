using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LayoutedReader.Infra
{
    public class FileLocator : IFileLocator
    {
        public Stream Open(string file)
        {
            return File.OpenRead(file);
        }
    }
}
