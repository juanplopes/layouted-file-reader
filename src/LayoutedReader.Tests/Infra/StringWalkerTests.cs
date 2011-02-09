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
    public class StringWalkerTests
    {
        [Test]
        public void can_walk_2_by_time_in_6_length_string()
        {
            var str = new StringWalker("abcdef");
            str.Read(2).Should().Be("ab");
            str.Read(2).Should().Be("cd");
            str.Read(2).Should().Be("ef");
        }

        [Test]
        public void can_walk_2_by_time_in5_length_string()
        {
            var str = new StringWalker("abcde");
            str.Read(2).Should().Be("ab");
            str.Read(2).Should().Be("cd");
            str.Read(2).Should().Be("e");
        }

        [Test]
        public void can_walk_2_by_time_in4_length_string()
        {
            var str = new StringWalker("abcd");
            str.Read(2).Should().Be("ab");
            str.Read(2).Should().Be("cd");
            str.Read(2).Should().Be("");
        }

        [Test]
        public void peek_doesn_t_change_position()
        {
            var str = new StringWalker("abcd");
            str.Read(2).Should().Be("ab");
            str.Position.Should().Be(2);
            str.Peek(2).Should().Be("cd");
            str.Position.Should().Be(2);
        }

        [Test]
        public void can_set_position()
        {
            var str = new StringWalker("abcd");
            str.Position = 2;
            str.Read(2).Should().Be("cd");
            
        }
    }
}
