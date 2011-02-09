using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LayoutedReader.Types;
using SharpTestsEx;

namespace LayoutedReader.Tests.Types
{
    [TestFixture]
    public class ConstantTests
    {
        [Test]
        public void can_read_date_as_datetime()
        {
            var constant = new ConstantType("2010-1-15, date");
            constant.Read(null).Should().Be(new DateTime(2010, 1, 15));
        }

        [Test]
        public void can_read_datetime_as_datetime()
        {
            var constant = new ConstantType("2010-1-15 22:55:56, date");
            constant.Read(null).Should().Be(new DateTime(2010, 1, 15, 22, 55, 56));
        }

        [Test]
        public void can_read_today_as_datetime()
        {
            var constant = new ConstantType("00:00:00, date");
            constant.Read(null).Should().Be(DateTime.Today);
        }

        [Test]
        public void can_read_number_as_int()
        {
            var constant = new ConstantType("2, int");
            constant.Read(null).Should().Be(2);
        }

        [Test]
        public void can_read_number_as_decimal()
        {
            var constant = new ConstantType("2, decimal");
            constant.Read(null).Should().Be(2m);
        }

        [Test]
        public void can_read_date_as_string()
        {
            var constant = new ConstantType("2010-1-15");
            constant.Read(null).Should().Be("2010-1-15");
        }
    }
}
