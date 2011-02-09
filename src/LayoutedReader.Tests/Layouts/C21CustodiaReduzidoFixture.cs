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
    public class C21CustodiaReduzidoFixture
    {
        private ImportedFile file;
        [TestFixtureSetUp]
        public void Setup()
        {
            var layout = SimpleSerializer.Xml<Layout>()
                .DeserializeTypedFromString(SampleLayouts.test_b_layout);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(SampleLayouts.test_b)))
                file = layout.ReadAll(stream);
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
            file.Records.Count.Should().Be(49);
        }

        [Test]
        public void header_must_be_correct()
        {
            file.Header["DCTIPO-REG-H"].Value.Should().Be("0");
            file.Header["DCDATULTMOV"].Value.Should().Be(new DateTime(2010, 12, 03));
            file.Header["DCNUMDIASUTEIS"].Value.Should().Be(3);
        }

        [Test]
        public void record_1()
        {
            AssertRow(file.Records[0],
                "1", new DateTime(2010, 12, 01), 74870008, "CCB", "05L00000142", 1m,
                000170018693251014m, 0m, 000187655873467410m, 000000000187655873m);

        }

        [Test]
        public void record_49()
        {
            AssertRow(file.Records[48],
                "1", new DateTime(2010, 12, 01), 79610005, "CCB", "06D00000414", 1m,
                000496045836614017m, 0m, 000498113867084281m, 000000000498113867m);
        }

        [Test]
        public void there_are_27_with_dcif_starting_with_05()
        {
            var expr = new BooExpression("c.dcif.StartsWith('05')");
            file.Records.Where(x => expr.AppliesTo(x)).Count().Should().Be(27);
        }

       
    }
}
