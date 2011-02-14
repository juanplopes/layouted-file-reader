using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Filters
{
    public class TrueFilter : IFilter
    {
        public static readonly TrueFilter Instance = new TrueFilter();

        public bool AppliesTo(IEnumerable<ValueBag> bags)
        {
            return true;
        }
    }
}
