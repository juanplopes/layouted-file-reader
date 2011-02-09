using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Types
{
    public class NullFormatter : IFormatter
    {
        public static NullFormatter Instance = new NullFormatter();

        #region IFormatter Members

        public NullFormatter() { }
        public NullFormatter(string args) { }

        public object Format(object obj)
        {
            return null;
        }

        #endregion
    }
}
