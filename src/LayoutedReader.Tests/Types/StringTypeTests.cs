using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LayoutedReader.Types;
using LayoutedReader.Infra;
using SharpTestsEx;

namespace LayoutedReader.Tests.Types
{
    [TestFixture]
    public class StringTypeTests
    {
        [Test]
        public void can_read_string_with_trim()
        {
            var type = new StringType("3");
            type.Read(new StringWalker("qw a")).Should().Be("qw");
        }

        [Test]
        public void can_read_string_without_trim()
        {
            var type = new StringType("3,false");
            type.Read(new StringWalker("qw a")).Should().Be("qw ");
        }

        [Test]
        public void can_read_string_passing_the_end_with_trim()
        {
            var type = new StringType("3");
            type.Read(new StringWalker("q ")).Should().Be("q");
        }

        [Test]
        public void can_read_string_passing_the_end_without_trim()
        {
            var type = new StringType("3,false");
            type.Read(new StringWalker("q ")).Should().Be("q ");
        }
    }
}
