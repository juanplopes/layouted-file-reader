using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Simple;

namespace LayoutedReader.Infra
{
    public class Parameters
    {
        static Regex DEFAULT_REGEX =
            new Regex("^{0}?(,{0})*$".AsFormatFor(@"\s*(?<p>[^,]*?)\s*"),
                RegexOptions.Compiled);
        public static Parameters Default
        {
            get { return new Parameters(DEFAULT_REGEX); }
        }

        Regex regex;
        public Parameters(Regex regex)
        {
            this.regex = regex;
        }

        public Parameters(string regex) : this(new Regex(regex)) { }

        public ParamsExtraction Extract(string input)
        {
            return new ParamsExtraction(regex.Match(input??"").Groups["p"]);
        }
    }
}
