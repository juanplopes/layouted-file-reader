using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LayoutedReader.Infra;

namespace LayoutedReader.Types
{
    public class ConstantType : IReader
    {
        public static ConstantType Instance = new ConstantType();
        object value;

        public ConstantType() { }
        public ConstantType(string args)
        {
            var extraction = Parameters.Default.Extract(args);
            var type = (extraction.Optional<string>(1) ?? "").ToLower();


            switch (type)
            {
                case "date":
                    value = extraction.Required<DateTime>(0);
                    break;
                case "int":
                    value = extraction.Required<int>(0);
                    break;
                case "decimal":
                    value = extraction.Required<decimal>(0);
                    break;
                default:
                    value = extraction.Required<string>(0);
                    break;
            }
        }
        public int Length { get { return 0; } }


        public object Read(StringWalker str)
        {
            return value;
        }
    }
}
