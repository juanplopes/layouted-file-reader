using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LayoutedReader;
using LayoutedReader.Infra;
using SharpTestsEx;
using LayoutedReader.Layouts;
using LayoutedReader.Types;

namespace LayoutedReader.Tests.Layouts
{
    [TestFixture]
    public class FieldTests
    {
        [Test]
        public void can_create_field_with_null_formatter()
        {
            var field = new Field("teste", "number(6)", "null");
            var bag = new ValueBag();
            field.Read(new StringWalker("123"), bag);
            bag["teste"].Formatter.Should().Be.OfType<NullFormatter>();
        }

        [Test]
        public void default_field_formatter_is_direct()
        {
            var field = new Field("teste", "number(6)", null);
            var bag = new ValueBag();
            field.Read(new StringWalker("123"), bag);
            bag["teste"].Formatter.Should().Be.OfType<DirectFormatter>();
        }

        [Test]
        public void default_header_formatter_is_null()
        {
            var field = new Header("teste", "number(6)", null);
            var bag = new ValueBag();
            field.Read(new StringWalker("123"), bag);
            bag["teste"].Formatter.Should().Be.OfType<NullFormatter>();
        }

        [Test]
        public void field_of_int_format_direct()
        {
            var field = new Field("teste", "number(6)", null);
            var bag = new ValueBag();
            field.Read(new StringWalker("1234567"), bag);

            bag.GetAs<decimal>("teste").Should().Be(123456m);
            bag["teste"].Value.Should().Be(123456);
            bag["teste"].Format().Should().Be(123456);
        }

        [Test]
        public void field_of_int_format_culture()
        {
            var field = new Field("teste", "number(6,1)", "string(F3,pt-br)");
            var bag = new ValueBag();
            field.Read(new StringWalker("12345678"), bag);

            bag.GetAs<decimal>("teste").Should().Be(123456.7m);
            bag["teste"].Value.Should().Be(123456.7m);
            bag["teste"].Format().Should().Be("123456,700");
        }

        [Test]
        public void field_of_datetime_format_culture()
        {
            var field = new Field("teste", "date(HH-dd/MM/yyyy)", "string(ddMMyyyyHH)");
            var bag = new ValueBag();
            field.Read(new StringWalker("22-15/01/2009"), bag);


            bag["teste"].Value.Should().Be(new DateTime(2009, 1, 15, 22, 0, 0));
            bag["teste"].Format().Should().Be("1501200922");
        }

        [Test]
        public void field_of_constant_datetime_format_culture()
        {
            var field = new Field("teste", "constant(2009-11-25, date)", "string(dd/MM/yyyy)");
            var bag = new ValueBag();
            var str = new StringWalker("12345678");
            field.Read(str, bag);

            bag["teste"].Value.Should().Be(new DateTime(2009, 11, 25));
            bag["teste"].Format().Should().Be("25/11/2009");
        }
    }
}
