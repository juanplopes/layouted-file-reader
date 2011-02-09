using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Filters
{
    public interface IFilter
    {
        bool AppliesTo(params ValueBag[] bags);
    }
}
