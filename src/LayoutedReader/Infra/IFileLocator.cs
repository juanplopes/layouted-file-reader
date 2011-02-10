using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LayoutedReader.Infra
{
    public interface IFileLocator
    {
        Stream OpenIndex();
        Stream OpenRelative(string file);
        Stream OpenAny(string file);
    }
}
