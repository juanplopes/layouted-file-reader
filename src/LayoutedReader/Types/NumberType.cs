using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LayoutedReader;
using System.Globalization;
using LayoutedReader.Infra;

namespace LayoutedReader.Types
{
    public class NumberType : IReader
    {
        public int Precision { get; private set; }
        public int Scale { get; private set; }

        public NumberType(string args)
        {
            var extraction = Parameters.Default.Extract(args);
            Precision = extraction.Required<int>(0);
            Scale = extraction.Optional<int>(1);
        }

        public object Read(StringWalker str)
        {
            decimal integral = decimal.Parse(str.Read(Precision));

            if (Scale == 0)
                if (Precision <= 10)
                    return (int)integral;
                else
                    return integral;
            else
                return integral + int.Parse(str.Read(Scale)) * b10[Scale];
        }

        public int Length { get { return Precision + Scale; } }


        static decimal[] b10;
        private const int MAX_SCALE = 64;
        static NumberType()
        {
            b10 = new decimal[MAX_SCALE];
            b10[0] = 1;
            for (int i = 1; i < MAX_SCALE; i++)
                b10[i] = b10[i - 1] / 10;
        }
    }
}
