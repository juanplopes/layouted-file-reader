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

        public ImportedFile ReadAll(Stream stream)
        {
            var reader = new StreamReader(stream);
            return new ImportedFile(
                ReadHeader(reader),
                ReadBody(reader).ToList());
        }

        public IEnumerable<RecordContext> Read(Stream stream)
        {
            var reader = new StreamReader(stream);
            var header = ReadHeader(reader);

            long estimatedTotal = stream.Length / (Fields.Sum(x => x.Length) + 1);

            var stopwatch = Stopwatch.StartNew();
            return ReadBody(reader).Select((x,i)=>
                new RecordContext(header, x, i+1, estimatedTotal, stream.Position, stream.Length, stopwatch.Elapsed));
        }

        protected ValueBag ReadHeader(TextReader reader)
        {
            var line = reader.ReadLine();
            return Read(line, HeaderFields, "header");
        }

        protected IEnumerable<ValueBag> ReadBody(TextReader reader)
        {
            return EnumerateReader(reader).Select(
                (x, i) => Read(x, Fields, string.Format("line#{0}", i + 1)));
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
