using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LayoutedReader.Types;

namespace LayoutedReader.Layouts
{
    public class Header : BaseField
    {
        public Header()
            : base(ConstantType.Instance, NullFormatter.Instance)
        {
        }

        public Header(string name, string type, string format)
            : this()
        {
            this.Name = name;
            this.Type = type;
            this.Format = format;
        }
    }
}
