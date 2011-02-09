using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using LayoutedReader.Types;
using LayoutedReader.Infra;

namespace LayoutedReader.Tests.Types
{
    [TestFixture]
    public class DateTypeTests
    {
        [Test]
        public void can_read_yyMMdd_year()
        {
            var date = new DateType("yyMMdd");
            date.Read(new StringWalker("991213")).Should().Be(new DateTime(1999, 12, 13));
        }

        [Test]
        public void can_read_yyyyMMddmmhh_year()
        {
            var date = new DateType("yyyyMMdd mmHH");
            date.Read(new StringWalker("19991213 5522")).Should().Be(new DateTime(1999, 12, 13, 22, 55, 00));
        }
    }
}
