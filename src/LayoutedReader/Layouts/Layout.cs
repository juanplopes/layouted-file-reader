using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using LayoutedReader.Infra;
using System.IO;
using LayoutedReader.Layouts;
using System.Threading;
using System.Diagnostics;

namespace LayoutedReader.Layouts
{
    [XmlRoot("layout")]
    public class Layout
    {
        [XmlElement("header")]
        public List<Header> HeaderFields { get; set; }

        [XmlElement("field")]
        public List<Field> Fields { get; set; }

        public Layout()
        {
            HeaderFields = new List<Header>();
            Fields = new List<Field>();
        }

        public FileContext<RecordContext> Read(Stream stream)
        {
            long estimatedTotal = stream.Length / (Fields.Sum(x => x.Length) + 1);
            
            var stopwatch = Stopwatch.StartNew();

            var reader = new StreamReader(stream);
            var headerRow = reader.ReadLine();
            var header = Read(headerRow, HeaderFields, "header");
            var headerCtx = new RecordContext(null, headerRow, header, 0, estimatedTotal, 
                stream.Position, stream.Length, stopwatch.Elapsed);



            return new FileContext<RecordContext>(
                EnumerateReader(reader).Select((row, i) =>
                new RecordContext(headerCtx, row, Read(row, Fields, string.Format("row#{0}", i + 1)), i + 1, estimatedTotal, stream.Position, stream.Length, stopwatch.Elapsed)));

        }

        private IEnumerable<string> EnumerateReader(TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
                yield return line;
        }


        private static ValueBag Read<T>(string line, IEnumerable<T> fieldList, string identifier)
            where T : BaseField
        {
            var bag = new ValueBag();
            var walker = new StringWalker(line);
            foreach (var field in fieldList)
            {
                try
                {
                    field.Read(walker, bag);
                }
                catch (Exception e)
                {
                    throw new FileLayoutException(string.Format("{0} ({1},{2})", identifier, field.Name, walker.Position), e);
                }
            }
            return bag;
        }
    }
}
