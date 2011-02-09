using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LayoutedReader;
using SharpTestsEx;
using LayoutedReader.Types;
using Moq;
using LayoutedReader.Infra;

namespace LayoutedReader.Tests.Infra
{
    [TestFixture]
    public class TypeConvertersTests
    {
        [Test]
        public void can_instantiate_number_reader()
        {
            var reader = TypeConverters.Default.CreateReader("number", "1,2");
            var assert = reader.Should().Be.OfType<NumberType>();
            assert.And.ValueOf.Precision.Should().Be(1);
            assert.And.ValueOf.Scale.Should().Be(2);
        }

        [Test]
        public void can_instantiate_unknown_reader()
        {
            var reader = TypeConverters.Default.CreateReader(typeof(SampleReader).AssemblyQualifiedName, "1,2");
            reader.Should().Be.OfType<SampleReader>();
        }

        [Test]
        public void can_instantiate_string_formatter()
        {
            var reader = TypeConverters.Default.CreateFormatter("string", "");
            reader.Should().Be.OfType<AsStringFormatter>();
        }

        [Test]
        public void can_instantiate_direct_formatter()
        {
            var reader = TypeConverters.Default.CreateFormatter("direct", "");
            reader.Should().Be.OfType<DirectFormatter>();
        }

        [Test]
        public void can_instantiate_unknown_formatter()
        {
            var reader = TypeConverters.Default.CreateFormatter(typeof(SampleFormatter).AssemblyQualifiedName, null);
            reader.Should().Be.OfType<SampleFormatter>();
        }

        public class SampleReader : IReader
        {
            public int Length { get { return 0; } }
            public SampleReader(string s) { }
            public object Read(StringWalker str) { throw new NotImplementedException(); }
        }

        public class SampleFormatter : IFormatter
        {
            public SampleFormatter(string s) { }
            public object Format(object obj) { throw new NotImplementedException(); }
        }
    }

}
