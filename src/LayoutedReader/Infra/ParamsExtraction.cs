using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Simple;
using System.Globalization;

namespace LayoutedReader.Infra
{
    public class ParamsExtraction
    {
        string[] values;
        public int Count { get { return values.Length; } }
        public IEnumerable<string> Params { get { return values; } }

        public ParamsExtraction(Group group)
        {
            this.values = group.Captures.Cast<Capture>()
                .Select(x => x.Value).ToArray();
        }

        public bool TryGet<T>(int position, out T result)
        {
            if (position < 0 || position >= Count) return Failure(out result);

            var str = values[position];
            if (IsNonStringNullValue<T>(str)) return Failure(out result);

            var type = typeof(T).GetValueTypeIfNullable();
            try
            {
                result = (T)Convert.ChangeType(str, type, CultureInfo.InvariantCulture);
            }
            catch(FormatException e)
            {
                throw new ArgumentException("couldn't format '{0}' as '{1}'".AsFormatFor(str, type), e);
            }
            return true;

        }

        private static bool Failure<T>(out T result)
        {
            result = default(T);
            return false;
        }

        private static bool IsNonStringNullValue<T>(string str)
        {
            return (typeof(T) != typeof(string) && str.Trim().IsNullOrEmpty());
        }

        public T Required<T>(int position)
        {
            T result;
            if (!TryGet(position, out result))
                throw new ArgumentOutOfRangeException("required positional parameter {0} not found".AsFormatFor(position));
            return result;
        }

        public T Optional<T>(int position)
        {
            T result;
            TryGet(position, out result);
            return result;
        }
    }
}
