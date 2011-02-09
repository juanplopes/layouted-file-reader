using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using LayoutedReader.Infra;
using System.Collections;
using Simple.Reflection;
using LayoutedReader.Types;

namespace LayoutedReader.Layouts
{
    public class Field : BaseField
    {
        public Field()
            : base(ConstantType.Instance, DirectFormatter.Instance)
        {
        }

        public Field(string name, string type, string format)
            : this()
        {
            this.Name = name;
            this.Type = type;
            this.Format = format;
        }
    }
}
