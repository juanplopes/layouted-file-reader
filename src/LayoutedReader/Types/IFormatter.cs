using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Types
{
    public interface IFormatter
    {
        object Format(object obj);
    }
}
