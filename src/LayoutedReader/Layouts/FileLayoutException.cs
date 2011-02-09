using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Layouts
{
    [Serializable]
    public class FileLayoutException : Exception
    {
        public FileLayoutException() { }

        public FileLayoutException(string id, Exception inner) :
            base(string.Format("{0}: {1}", id, inner.Message), inner) { }
        protected FileLayoutException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
