using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Layouts
{
    public class FileContext<T> : IDisposable, IEnumerable<T>
    {
        public T HeaderContext { get; private set; }
        IEnumerable<T> enumerable;

   

        public FileContext(T header, IEnumerable<T> enumerable)
        {
            this.HeaderContext = header;
            this.enumerable = enumerable;
        }


        public FileContext<T> CompleteInitialize()
        {
            return new FileContext<T>(HeaderContext, enumerable.ToList());
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
