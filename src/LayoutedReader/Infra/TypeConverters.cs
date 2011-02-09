using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LayoutedReader.Types;
using Simple.Reflection;
using Simple;

namespace LayoutedReader.Infra
{
    public class TypeConverters
    {
        static TypeConverters defaultInstance;
        public static TypeConverters Default { get { return defaultInstance; } }
        static TypeConverters()
        {
            defaultInstance = new TypeConverters();
            defaultInstance.Readers["number"] = typeof(NumberType);
            defaultInstance.Readers["constant"] = typeof(ConstantType);
            defaultInstance.Readers["date"] = typeof(DateType);
            defaultInstance.Readers["string"] = typeof(StringType);

            defaultInstance.Formatters["string"] = typeof(AsStringFormatter);
            defaultInstance.Formatters["direct"] = typeof(DirectFormatter);
            defaultInstance.Formatters["null"] = typeof(NullFormatter);
        }

        IDictionary<string, Type> readers = new Dictionary<string, Type>();
        IDictionary<string, Type> formatters = new Dictionary<string, Type>();

        public IDictionary<string, Type> Readers { get { return readers; } }
        public IDictionary<string, Type> Formatters { get { return formatters; } }

        MethodCache cache = new MethodCache();

        public virtual IReader CreateReader(string type, string args)
        {
            return Create<IReader>(readers, type, args);
            
        }

        public virtual IFormatter CreateFormatter(string type, string args)
        {
            return Create<IFormatter>(formatters, type, args);
        }

        private T Create<T>(IDictionary<string, Type> dic, string type, string args)
        {
            type = type.Trim();
            Type objType;
            if (!dic.TryGetValue(type, out objType))
                objType = Type.GetType(type);

            return (T)cache.CreateInstance(objType, args);
        }
    }
}
