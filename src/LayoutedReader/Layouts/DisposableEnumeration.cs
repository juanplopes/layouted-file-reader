using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Layouts
{
    public class DisposableEnumeration<T> : IDisposable, IEnumerable<T>
    {
        IEnumerable<T> enumerable;
        public DisposableEnumeration(IEnumerable<T> enumerable)
        {
            this.enumerable = enumerable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return enumerable.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return enumerable.GetEnumerator();
        }

        public void Dispose()
        {
            if (enumerable is IDisposable)
                (enumerable as IDisposable).Dispose();
        }
    }
}
