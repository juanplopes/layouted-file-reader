using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LayoutedReader;
using Simple.IO.Serialization;
using System.IO;
using Simple;
using SharpTestsEx;
using LayoutedReader.Filters;
using LayoutedReader.Layouts;
using LayoutedReader.Tests.Layouts.Examples;

namespace LayoutedReader.Tests.Layouts
{
    [TestFixture]
    public class BReduzidoFixture
    {
        private IList<RecordContext> file;
        [TestFixtureSetUp]
        public void Setup()
        {
            var layout = SimpleSerializer.Xml<Layout>()
                .DeserializeTypedFromString(SampleLayouts.test_b_layout);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(SampleLayouts.test_b)))
                file = layout.Read(stream).ToList();
        }

        static string[] columns = 
        {
           "dctipo-reg-d", "dcdatop", "dcdetentor", "dctipoif",
           "dcif", "dcquantidade", "dcvalornominal", "dcvalorjurosdia",
           "dcpucurva", "dcvalor"
        };
        private void AssertRow(ValueBag bag, params object[] values)
        {
            bag.Count.Should().Be.Equals(values.Length);
            for (int i = 0; i < columns.Length; i++)
                bag[columns[i]].Value.Should().Be(values[i]);
        }

        [Test]
        public void must_have_49_records()
        {
            file.Count.Should().Be(49);
        }

        [Test]
        public void header_must_be_correct()
        {
            file[0].Header["DCTIPO-REG-H"].Value.Should().Be("0");
            file[0].Header["DCDATULTMOV"].Value.Should().Be(new DateTime(2010, 12, 03));
            file[0].Header["DCNUMDIASUTEIS"].Value.Should().Be(3);
        }

        [Test]
        public void record_1()
        {
            AssertRow(file[0].Record,
                "1", new DateTime(2010, 12, 01), 74870008, "CCB", "05L00000142", 1m,
                000170018693251014m, 0m, 000187655873467410m, 000000000187655873m);

        }

        [Test]
        public void record_49()
        {
            AssertRow(file[48].Record,
                "1", new DateTime(2010, 12, 01), 79610005, "CCB", "06D00000414", 1m,
                000496045836614017m, 0m, 000498113867084281m, 000000000498113867m);
        }

        [Test]
        public void there_are_27_with_dcif_starting_with_05()
        {
            var expr = new BooExpression("c.dcif.StartsWith('05')");
            file.Where(x => expr.AppliesTo(x.Record)).Count().Should().Be(27);
        }

       
    }
}
