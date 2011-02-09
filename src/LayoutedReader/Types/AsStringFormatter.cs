using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using LayoutedReader;
using Simple;
using LayoutedReader.Infra;

namespace LayoutedReader.Types
{
    public class AsStringFormatter : IFormatter
    {
        public CultureInfo Culture { get; private set; }
        public string FormatString { get; private set; }
        public AsStringFormatter(string args)
        {
            var extraction = Parameters.Default.Extract(args);
            FormatString = extraction.Optional<string>(0);
            string cultureName = extraction.Optional<string>(1);
            if (cultureName != null)
                Culture = CultureInfo.GetCultureInfo(cultureName);
            else
                Culture = CultureInfo.InvariantCulture;

        }

        public object Format(object obj)
        {
            if (obj == null) return null;

            var formattable = (IFormattable)obj;

            return formattable.ToString(FormatString, Culture);
        }
    }
}
