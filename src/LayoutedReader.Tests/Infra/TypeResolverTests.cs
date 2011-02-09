using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using LayoutedReader.Infra;
using LayoutedReader.Types;
using SharpTestsEx;
using LayoutedReader;

namespace LayoutedReader.Tests.Infra
{
    [TestFixture]
    public class TypeResolverTests
    {
        [Test]
        public void can_parse_default_number()
        {
            var number = TypeResolver.Default.CreateReader("number(6)")
                .Should().Be.OfType<NumberType>().And.ValueOf;

            number.Precision.Should().Be(6);
            number.Scale.Should().Be(0);
        }

        [Test]
        public void can_parse_number_integer()
        {
            var number = AssertTypeResolver("numb", "1,4", "numb(1,4)", new NumberType("1,4"));

            number.Precision.Should().Be(1);
            number.Scale.Should().Be(4);
        }

        [Test]
        public void can_parse_number_integer_with_space_in_the_end()
        {
            var number = AssertTypeResolver("numb ", "1,4", "numb (1,4)", new NumberType("1,4"));

            number.Precision.Should().Be(1);
            number.Scale.Should().Be(4);
        }

        [Test]
        public void can_parse_number_integer_with_many_spaces()
        {
            var number = AssertTypeResolver("   numb ", " 1   ,   4", "   numb ( 1   ,   4)   ", new NumberType("1,4"));

            number.Precision.Should().Be(1);
            number.Scale.Should().Be(4);
        }

        [Test]
        public void can_parse_formatter()
        {
            var number = AssertFormatterResolver("numb", "asd,pt-br", "numb(asd,pt-br)", 
                new AsStringFormatter("asd,pt-br"));

            number.Culture.Name.Should().Be("pt-BR");
            number.FormatString.Should().Be("asd");
        }




        private static T AssertTypeResolver<T>(string a, string b, string full, T reader)
            where T : IReader
        {
            var mock = new Mock<TypeConverters>();
            mock.Setup(x => x.CreateReader(a, b)).Returns(reader);
            var resolver = new TypeResolver(mock.Object);
            var number = resolver.CreateReader(full);
            mock.VerifyAll();
            return number.Should().Be.OfType<T>().And.ValueOf;
            
        }

        private static T AssertFormatterResolver<T>(string a, string b, string full, T reader)
            where T : IFormatter
        {
            var mock = new Mock<TypeConverters>();
            mock.Setup(x => x.CreateFormatter(a, b)).Returns(reader);
            var resolver = new TypeResolver(mock.Object);
            var number = resolver.CreateFormatter(full);
            mock.VerifyAll();
            return number.Should().Be.OfType<T>().And.ValueOf;

        }
    }
}
