using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Types
{
    public class DirectFormatter : IFormatter
    {
        public static DirectFormatter Instance = new DirectFormatter(null);

        public DirectFormatter(string args) { }

        public object Format(object obj)
        {
            return obj;
        }
    }
}
