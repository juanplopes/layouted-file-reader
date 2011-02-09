using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LayoutedReader;
using LayoutedReader.Infra;

namespace LayoutedReader.Types
{
    public interface IReader
    {
        object Read(StringWalker str);
        int Length { get; }
    }
}
