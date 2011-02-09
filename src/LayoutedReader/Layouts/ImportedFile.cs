using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Layouts
{
    public class ImportedFile
    {
        public ValueBag Header { get; private set; }
        public IList<ValueBag> Records { get; private set; }
        public ImportedFile(ValueBag header, IList<ValueBag> fields)
        {
            this.Header = header;
            this.Records = fields;
        }
    }
}
