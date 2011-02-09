using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Simple;
using LayoutedReader.Types;

namespace LayoutedReader.Infra
{
    public class TypeResolver
    {
        static TypeResolver defaultInstance = new TypeResolver(TypeConverters.Default);
        public static TypeResolver Default { get { return defaultInstance; } }

        static Regex regex = new Regex(@"^{0}(\({0}\)\s*)?$".AsFormat(@"(?<p>.+?)"));

        TypeConverters converters;
        public TypeResolver(TypeConverters converters)
        {
            this.converters = converters;
        }

        public IReader CreateReader(string identifier)
        {
            return CreateGeneric(identifier, (x, y) => converters.CreateReader(x, y));
        }

        public IFormatter CreateFormatter(string identifier)
        {
            return CreateGeneric(identifier, (x, y) => converters.CreateFormatter(x, y));
        }

        private T CreateGeneric<T>(string identifier, Func<string, string, T> func)
            where T : class
        {
            if (identifier == null) return null;
            var match = regex.Match(identifier).Groups["p"];
            if (!match.Success)
                throw new ArgumentException("the type '{0}' is invalid".AsFormat(identifier));

            var type = match.Captures[0].Value;

            string args = null;
            if (match.Captures.Count > 1)
                args = match.Captures[1].Value;
            return func(type, args);
        }
    }
}
