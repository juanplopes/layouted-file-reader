using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LayoutedReader;
using SharpTestsEx;
using LayoutedReader.Infra;

namespace LayoutedReader.Tests.Infra
{
    [TestFixture]
    public class ParametersTests
    {
        [Test]
        public void read_2_default_parameter_all_required()
        {
            var param = Parameters.Default;
            var extraction = param.Extract("450, 1");

            extraction.Required<int>(0).Should().Be(450);
            extraction.Required<int>(1).Should().Be(1);
        }


        [Test]
        public void read_2_default_parameter_all_wrong_type()
        {
            var param = Parameters.Default;
            var extraction = param.Extract("asd, qwe");

            extraction.Executing(x => x.Required<int>(0)).Throws<ArgumentException>()
                .And.Exception.Message.Should().Be("couldn't format 'asd' as 'System.Int32'");
            extraction.Executing(x => x.Required<int>(1)).Throws<ArgumentException>()
                .And.Exception.Message.Should().Be("couldn't format 'qwe' as 'System.Int32'");
        }
        [Test]
        public void read_3_default_parameter_two_required()
        {
            var param = Parameters.Default;
            var extraction = param.Extract("450, , 451");

            extraction.Required<int>(0).Should().Be(450);
            extraction.Optional<int?>(1).Should().Be(null);
            extraction.Optional<int?>(2).Should().Be(451);
        }

        [Test]
        public void read_3_default_parameter_last_optional()
        {
            var param = Parameters.Default;
            var extraction = param.Extract("450,20");

            extraction.Optional<int>(0).Should().Be(450);
            extraction.Optional<int>(1).Should().Be(20);
            extraction.Optional<int?>(2).Should().Be(null);
        }

        [Test]
        public void read_3_default_parameter_all_empty()
        {
            var param = Parameters.Default;
            var extraction = param.Extract(",,");

            extraction.Optional<DateTime?>(0).Should().Be(null);
            extraction.Optional<int?>(1).Should().Be(null);
            extraction.Optional<int?>(2).Should().Be(null);
        }

        [Test]
        public void read_3_string_only_default_parameters()
        {
            var param = Parameters.Default;
            var extraction = param.Extract("abc, def , qwe ");

            extraction.Optional<string>(0).Should().Be("abc");
            extraction.Optional<string>(1).Should().Be("def");
            extraction.Optional<string>(2).Should().Be("qwe");
        }


        [Test]
        public void read_non_passed_int_value_throws_exception()
        {
            var param = Parameters.Default;
            var extraction = param.Extract("  ,");

            extraction.Required<string>(0).Should().Be("");
            extraction.Executing(x => x.Required<int>(1))
                .Throws<ArgumentOutOfRangeException>();
        }

        [Test]
        public void read_empty_string()
        {
            var param = Parameters.Default;
            var extraction = param.Extract("");

            extraction.Optional<int?>(0).Should().Be(null);
        }

        [Test]
        public void read_null_string()
        {
            var param = Parameters.Default;
            var extraction = param.Extract(null);

            extraction.Optional<int?>(0).Should().Be(null);
        }
    }
}
