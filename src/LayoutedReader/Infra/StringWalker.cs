using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayoutedReader.Infra
{
    public class StringWalker
    {
        string buffer;
        public int Position { get; set; }
        public StringWalker(string str)
        {
            this.buffer = str;
        }

        public string Peek(int size)
        {
            return buffer.Substring(Position, Math.Min(size, buffer.Length - Position));
        }

        public string Read(int size)
        {
            var ret = Peek(size);
            Position += ret.Length;
            return ret;
        }
    }
}
