using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LayoutedReader;
using System.Globalization;
using LayoutedReader.Infra;

namespace LayoutedReader.Types
{
    public class StringType : IReader
    {
        public int Length { get; private set; }
        public bool Trim { get; private set; }

        public StringType(string args)
        {
            var extraction = Parameters.Default.Extract(args);
            Length = extraction.Required<int>(0);
            Trim = extraction.Optional<bool?>(1)??true;
        }

        public object Read(StringWalker str)
        {
            var value = str.Read(Length);
            if (Trim) value = value.Trim();
            return value;
        }

    }
}
