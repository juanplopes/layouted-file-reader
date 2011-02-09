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
    public class AsStringFormatterTests
    {
        [Test]
        public void will_not_format_null_values()
        {
            var formatter = new AsStringFormatter(null);
            formatter.Format(null).Should().Be(null);
        }

        [Test]
        public void can_format_number_without_format_nor_culture()
        {
            var formatter = new AsStringFormatter(null);
            formatter.Format(123.456).Should().Be("123.456");
        }

        [Test]
        public void can_format_number_with_empty_string_format_nor_culture()
        {
            var formatter = new AsStringFormatter("");
            formatter.Format(123.456).Should().Be("123.456");
        }

        [Test]
        public void can_format_number_with_culture_only()
        {
            var formatter = new AsStringFormatter(",pt-br");
            formatter.Format(123.456).Should().Be("123,456");
        }

        [Test]
        public void can_format_number_with_format_only()
        {
            var formatter = new AsStringFormatter("F2");
            formatter.Format(123.456).Should().Be("123.46");
        }

        [Test]
        public void can_format_number_with_culture_and_currencyformat()
        {
            var formatter = new AsStringFormatter("C,pt-br");
            formatter.Format(123.456).Should().Be("R$ 123,46");
        }

        [Test]
        public void can_format_number_with_culture_and_floating_format()
        {
            var formatter = new AsStringFormatter("F2,pt-br");
            formatter.Format(123.456).Should().Be("123,46");
        }
    }
}
