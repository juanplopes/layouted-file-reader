using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LayoutedReader.Infra;
using System.Globalization;

namespace LayoutedReader.Types
{
    public class DateType : IReader
    {
        public string FormatString { get; set; }

        public DateType(string args)
        {
            var extraction = Parameters.Default.Extract(args);
            FormatString = extraction.Required<string>(0);
        }

        #region IReader Members

        public object Read(StringWalker str)
        {
            var value = str.Read(FormatString.Length);
            if (value == new string('0', FormatString.Length)) return null;

            return DateTime.ParseExact(value, FormatString, CultureInfo.InvariantCulture);
        }

        public int Length { get { return FormatString.Length; } }

        #endregion
    }
}
