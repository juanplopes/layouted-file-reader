using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Layouts
{
    public class DisposableEnumerable<T> : IDisposable, IEnumerable<T>
    {
        IEnumerable<T> enumerable;
        public DisposableEnumerable(IEnumerable<T> enumerable)
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
