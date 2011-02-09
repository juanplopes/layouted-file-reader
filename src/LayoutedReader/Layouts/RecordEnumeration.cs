using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Layouts
{
    public class RecordEnumeration : IDisposable, IEnumerable<DeployContext>
    {
        string filename;
        public string Filename { get { return filename; } }
        IEnumerable<DeployContext> enumerable;
        public RecordEnumeration(string filename, IEnumerable<DeployContext> enumerable)
        {
            this.enumerable = enumerable;
            this.filename = filename;
        }

        public IEnumerator<DeployContext> GetEnumerator()
        {
            return enumerable.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return enumerable.GetEnumerator();
        }

        public void Dispose()
        {
            (enumerable as IDisposable).Dispose();
        }
    }
}
