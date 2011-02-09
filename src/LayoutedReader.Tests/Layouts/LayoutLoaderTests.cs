﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LayoutedReader;
using NUnit.Framework;
using Moq;
using LayoutedReader.Infra;
using System.IO;
using LayoutedReader.Tests.Layouts;
using SharpTestsEx;
using LayoutedReader.Layouts;
using LayoutedReader.Tests.Layouts.Examples;

namespace LayoutedReader.Tests.Layouts
{
    public class LayoutLoaderTests
    {
        private Stream StreamFor(string content) {
            return new MemoryStream(Encoding.UTF8.GetBytes(content));
        }

        FileLoader loader;
        [TestFixtureSetUp]
        public void Setup()
        {
            var fs = new Mock<IFileLocator>();
            fs.Setup(x => x.Open(@"c:\a\b\index.xml")).Returns(StreamFor(SampleLayouts.index));
            fs.Setup(x => x.Open(@"c:\a\b\layouts\A.xml")).Returns(StreamFor(SampleLayouts.test_a_layout));
            fs.Setup(x => x.Open(@"c:\a\b\layouts\B.xml")).Returns(StreamFor(SampleLayouts.test_b_layout));
            fs.Setup(x => x.Open(@"c:\A.txt")).Returns(StreamFor(SampleLayouts.test_a));
            fs.Setup(x => x.Open(@"c:\B.txt")).Returns(StreamFor(SampleLayouts.test_b));
            fs.Setup(x => x.Open(@"c:\B2.txt")).Returns(StreamFor(SampleLayouts.test_b));

            loader = new FileLoader(fs.Object, @"c:\a\b\index.xml");
        }

        [Test]
        public void can_open_c21_file_with_correct_layout()
        {
            var c21 = loader.Read(@"c:\A.txt").ToList();
            c21.Count.Should().Be(49);
            c21[0].Record.Count.Should().Be(25);
        }

        [Test]
        public void can_open_c21_custodia_file_with_correct_layout()
        {
            var c21 = loader.Read(@"c:\B.txt").ToList();
            c21.Count.Should().Be(49);
            c21[0].Record.Count.Should().Be(10);
        }

        [Test]
        public void opening_a_non_mapped_file_results_in_file_not_found_exception()
        {
            loader.Executing(x => x.Read(@"c:\B2.txt").Any())
                .Throws<FileNotFoundException>();
        }
    }
}
