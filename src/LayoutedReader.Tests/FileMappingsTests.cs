using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Cetip.FileReader;
using SharpTestsEx;

namespace Cetip.Tests
{
    [TestFixture]
    public class FileMappingsTests
    {
        [Test]
        public void can_resolve_correct_case_file()
        {
            var mapping = new FileMappings()
            {
                Mappings = new List<FileMapping> { new FileMapping("abc.xml", "qwe.xml") }
            };
            mapping.LayoutFor("abc.xml").Should().Be("qwe.xml");
        }


        [Test]
        public void can_resolve_different_case_file()
        {
            var mapping = new FileMappings()
            {
                Mappings = new List<FileMapping> { new FileMapping("abc.xml", "qwe.xml") }
            };
            mapping.LayoutFor("ABC.xml").Should().Be("qwe.xml");
        }

        [Test]
        public void can_resolve_rooted_different_case_file()
        {
            var mapping = new FileMappings()
            {
                Mappings = new List<FileMapping> { new FileMapping("abc.xml", "qwe.xml") }
            };
            mapping.LayoutFor(@"root/ABC.xml").Should().Be("qwe.xml");
        }
    }
}
