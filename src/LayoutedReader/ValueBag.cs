using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Simple.Reflection;
using System.Linq.Expressions;


namespace LayoutedReader
{
    public class ValueBag : Dictionary<string, ValueItem>
    {
        public ValueBag() : base(KeyComparer.Instance) { }
        public ValueBag(SerializationInfo info, StreamingContext context) : base(info, context) { }


        public ValueBag(params IDictionary<string, ValueItem>[] dictionaries)
            : this((IEnumerable<IDictionary<string, ValueItem>>)dictionaries)
        {

        }

        public ValueBag(IEnumerable<ValueBag> dictionaries)
            : this(dictionaries.Cast<IDictionary<string, ValueItem>>()) { }

        public ValueBag(IEnumerable<IDictionary<string, ValueItem>> dictionaries)
            : this()
        {
            foreach (var dictionary in dictionaries.Where(x => x != null))
                foreach (var entry in dictionary)
                    this[entry.Key] = entry.Value;
        }

        public ValueItem Set(string key, object value)
        {
            return Set(key, value, null);
        }

        public ValueItem Set(string key, object value, Types.IFormatter formatter)
        {
            return this[key] = new ValueItem(value, formatter);
        }

        public ValueBag Clone()
        {
            return new ValueBag(this);
        }

        public T GetAs<T>(string key)
        {
            ValueItem value;
            if (TryGetValue(key, out value))
                return value.As<T>();
            else
                return default(T);
        }

        public static ValueBag Create(params Expression<Func<object, object>>[] exprs)
        {
            return new ValueBag(
                DictionaryHelper.FromExpressions(exprs).ToDictionary(x => x.Key, x => new ValueItem(x.Value)));
        }

        [Serializable]
        public class KeyComparer : StringComparer
        {
            public static KeyComparer Instance = new KeyComparer();
            static StringComparer internalComparer = StringComparer.InvariantCultureIgnoreCase;

            public static string IdentifierFormat(string identifier)
            {
                var chars = identifier.ToCharArray();
                for (int i = 0; i < identifier.Length; i++)
                {
                    var c = chars[i];
                    if (!(c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '_' || (c >= '0' && c <= '9' && i > 0)))
                        chars[i] = '_';
                }
                return new string(chars);
            }

            public override int Compare(string x, string y)
            {
                return internalComparer.Compare(IdentifierFormat(x), IdentifierFormat(y));
            }

            public override bool Equals(string x, string y)
            {
                return internalComparer.Equals(IdentifierFormat(x), IdentifierFormat(y));
            }

            public override int GetHashCode(string obj)
            {
                return internalComparer.GetHashCode(IdentifierFormat(obj));
            }
        }
    }
}
