using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LayoutedReader;
using LayoutedReader.Types;
using SharpTestsEx;
using LayoutedReader.Infra;

namespace LayoutedReader.Tests.Types
{
    [TestFixture]
    public class NumberTypeTests
    {
        [Test]    
        public void can_read_implicit_integral_type_from_string()
        {
            var str = new StringWalker("12345");
            var number = new NumberType("4");
            number.Read(str).Should().Be.OfType<int>().And.Be(1234);
        }

        [Test]
        public void can_read_explicit_integral_type_from_string()
        {
            var str = new StringWalker("123456789");
            var number = new NumberType("4");
            number.Read(str).Should().Be.OfType<int>().And.Be(1234);
        }

        [Test]
        public void can_read_implicit_decimal_type_from_string()
        {
            var str = new StringWalker("123456789");
            var number = new NumberType("4,3");
            number.Read(str).Should().Be.OfType<decimal>().And.Be(1234.567m);
        }

        [Test]
        public void can_read_explicit_decimal_type_from_string()
        {
            var str = new StringWalker("123456789");
            var number = new NumberType("4,3");
            number.Read(str).Should().Be.OfType<decimal>().And.Be(1234.567m);
        }

      
    }
}
