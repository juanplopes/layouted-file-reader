using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using LayoutedReader.Infra;
using LayoutedReader.Types;
using Simple.Reflection;
using System.Collections;

namespace LayoutedReader.Layouts
{
    public abstract class BaseField
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        private IReader reader;
        string type;

        [XmlAttribute("type")]
        public string Type
        {
            get { return type; }
            set { type = value; reader = TypeResolver.Default.CreateReader(type) ?? reader; }
        }

        private IFormatter formatter;
        string format;

        [XmlAttribute("format")]
        public string Format
        {
            get { return format; }
            set { format = value; formatter = TypeResolver.Default.CreateFormatter(format) ?? formatter; }
        }

        public int Length { get { return reader.Length; } }

        public BaseField(IReader reader, IFormatter formatter)
        {
            this.reader = reader;
            this.formatter = formatter;
        }

        public void Read(StringWalker walker, ValueBag bag)
        {
            bag.Set(Name, reader.Read(walker), formatter);
        }

        static IEqualityComparer comparer = new EqualityHelper<BaseField>()
            .Add(x => x.Type).Add(x => x.Format).Add(x => x.Name);

        public override bool Equals(object obj)
        {
            return comparer.Equals(this, obj);
        }

        public override int GetHashCode()
        {
            return comparer.GetHashCode(this);
        }
    }
}
