using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LayoutedReader;
using LayoutedReader.Types;
using Simple;

namespace LayoutedReader
{
    public struct ValueItem
    {
        object value;
        public object Value { get { return value; } }
        IFormatter formatter;
        public IFormatter Formatter { get { return formatter; } }

        public ValueItem(object value) : this(value, null) { }
        public ValueItem(object value, IFormatter formatter)
        {
            this.value = value;
            this.formatter = formatter ?? DirectFormatter.Instance;
        }

        public T As<T>()
        {
            if (value is IConvertible && typeof(T).CanAssign<IConvertible>())
                return (T)Convert.ChangeType(value, typeof(T));
            else
                return (T)(this.value ?? default(T));
        }

        public object Format()
        {
            return formatter.Format(this.value);
        }

        public override string ToString()
        {
            return (value ?? "<null>").ToString() + " (" + (Format() ?? "<null>").ToString() + ")";
        }
    }
}
