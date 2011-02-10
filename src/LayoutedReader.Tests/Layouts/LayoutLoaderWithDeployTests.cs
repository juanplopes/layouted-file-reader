using System;
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
    public class LayoutLoaderWithDeployTests
    {
        private Stream StreamFor(string content)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(content));
        }

        FileLoader loader;
        [TestFixtureSetUp]
        public void Setup()
        {
            var fs = new Mock<IFileLocator>();
            fs.Setup(x => x.OpenIndex()).Returns(() => StreamFor(SampleLayouts.index_deploy));
            fs.Setup(x => x.OpenRelative(@"layouts\A.xml")).Returns(() => StreamFor(SampleLayouts.test_a_layout));
            fs.Setup(x => x.OpenRelative(@"layouts\B.xml")).Returns(() => StreamFor(SampleLayouts.test_b_layout));
            fs.Setup(x => x.OpenRelative(@"deploys\Ad.xml")).Returns(() => StreamFor(SampleLayouts.test_a_deploy));
            fs.Setup(x => x.OpenRelative(@"deploys\Bd.xml")).Returns(() => StreamFor(SampleLayouts.test_b_deploy));
            fs.Setup(x => x.OpenAny(@"A.txt")).Returns(() => StreamFor(SampleLayouts.test_a));
            fs.Setup(x => x.OpenAny(@"B.txt")).Returns(() => StreamFor(SampleLayouts.test_b));
            fs.Setup(x => x.OpenAny(@"B2.txt")).Returns(() => StreamFor(SampleLayouts.test_b));

            loader = new FileLoader(fs.Object);
        }

        [Test]
        public void can_open_A_file_with_correct_layout()
        {
            var A = loader.Read(@"A.txt").ToList();
            A.Count.Should().Be(49);
            A[0].Record.Count.Should().Be(25);
        }

        [Test]
        public void when_opening_A_file_filter_correct_sem_coobrigacao()
        {
            var A = loader.Read(@"A.txt").ToList();
            var sem = A.Where(x => x.Record.GetAs<string>("mvcoobrigacao") == "SEM COOBRIGACAO");
            sem.Count().Should().Be(21);
            sem.Sum(x => x.Expanded.Count).Should().Be(21);
        }

        [Test]
        public void when_opening_A_file_filter_correct_integral()
        {
            var A = loader.Read(@"A.txt").ToList();
            var com = A.Where(x => x.Record.GetAs<string>("mvcoobrigacao") == "INTEGRAL");
            com.Count().Should().Be(28);
            com.Sum(x => x.Expanded.Count).Should().Be(55);

            var costa = A.Where(x => x.Record.GetAs<string>("MVLANC_COMPRADOR") == "COSTA");
            costa.Count().Should().Be(1);
            costa.Sum(x => x.Expanded.Count).Should().Be(1);
        }

        [Test]
        public void can_open_B_file_with_correct_layout()
        {
            var A = loader.Read(@"B.txt").ToList();
            A.Count.Should().Be(49);
            A[0].Record.Count.Should().Be(10);
        }

        [Test]
        public void opening_a_non_mapped_file_results_in_file_not_found_exception()
        {
            loader.Executing(x => x.Read(@"B2.txt").Any())
                .Throws<FileNotFoundException>();
        }
    }
}
