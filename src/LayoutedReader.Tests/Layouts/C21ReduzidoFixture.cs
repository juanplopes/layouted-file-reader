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
    public class C21ReduzidoFixture
    {
        private ImportedFile file;
        [TestFixtureSetUp]
        public void Setup()
        {
            var layout = SimpleSerializer.Xml<Layout>()
                .DeserializeTypedFromString(SampleLayouts.test_a_layout);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(SampleLayouts.test_a)))
                file = layout.ReadAll(stream);
        }

        static string[] columns = 
        {
            "MVTIPO-IF",
            "MVCOMPRADOR-CODIGO",
            "MVVENDEDOR-CODIGO",
            "MVIF-CODIGO",
            "MVDATA-EMISSAO",
            "MVDATA-VENCIMENTO",
            "MVCOOBRIGACAO",
            "MVOPERACAO-DATA",
            "MVOPERACAO-CODIGO",
            "MVSITUACAO",
            "MVOPERACAO-NUMERO",
            "MVOPERACAO-ORIGINAL",
            "MVVALOR-NOMINAL",
            "MVQUANTIDADE",
            "MVVALOR-FINANCEIRO",
            "MVMODALIDADE-LIQUIDACAO",
            "MVDATA-LIQUIDACAO",
            "MVBCOLIQ-COMPRADOR",
            "MVBCOLIQ-VENDEDOR",
            "MVLANC-COMPRADOR",
            "MVDATA-LANC-COMPRADOR",
            //"MVHORA-LANC-COMPRADOR",
            "MVLANC-VENDEDOR",
            "MVDATA-LANC-VENDEDOR",
            //"MVHORA-LANC-VENDEDOR",
            "MVDATA-COMPROMISSO-IDA",
            "MVDATA-COMPROMISSO-VOLTA"
        };
        private void AssertRow(ValueBag bag, params object[] values)
        {
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
            file.Header["ACUMMOV-DATULTMOV"].Value.Should().Be(new DateTime(2010, 12, 03));
        }

        [Test]
        public void record_1()
        {
            AssertRow(file.Records[0],
                "CCB", 18465008, 18465008, "10I00008111", new DateTime(2010, 09, 27),
                new DateTime(2012, 10, 24), "INTEGRAL", new DateTime(2010, 12, 01), 0001, 043,
                2010120119263637m, 0m, 0000002000m, 00000000000001m, 0000000000000000m,
                0, new DateTime(2010, 12, 1), 0, 0, "COSTA", new DateTime(2010, 12, 01, 18, 25, 11),
                "", new DateTime(2010, 12, 01, 18, 25, 11), 0, 0);
        }

        [Test]
        public void record_49()
        {
            AssertRow(file.Records[48],
                "CCB", 74220407, 74220005, "08H00018061", new DateTime(2008, 08, 15),
                new DateTime(2011, 08, 01), "INTEGRAL", new DateTime(2010, 12, 01), 0074, 043,
                2010113019254444m, 0m, 0000036110.1m, 00000000000001m, 00000000036110.10m,
                0, new DateTime(2010, 12, 1), 0, 0, "", new DateTime(2010, 11, 30, 23, 28, 39),
                "", new DateTime(2010, 11, 30, 23, 28, 39), 0, 0);
        }


        [Test]
        public void there_are_21_with_sem_coobrigacao()
        {
            var expr = new BooExpression("c.mvcoobrigacao == 'SEM COOBRIGACAO'");
            file.Records.Where(x => expr.AppliesTo(x)).Count().Should().Be(21);
        }

        [Test]
        public void there_are_9_with_mvdata_emissao_april()
        {
            var expr = new BooExpression("c.mvdata_emissao.Month == 4");
            file.Records.Where(x => expr.AppliesTo(x)).Count().Should().Be(9);
        }


        [Test]
        public void there_are_9_with_mvdata_emissao_2010_04_16()
        {
            var expr = new BooExpression("c.mvdata_emissao == DateTime(2010, 4, 16)");
            file.Records.Where(x => expr.AppliesTo(x)).Count().Should().Be(9);
        }
    }
}
