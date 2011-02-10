using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Serialization;
using LayoutedReader.Layouts;
using LayoutedReader.Tests.Layouts.Examples;
using Simple;
using SharpTestsEx;

namespace LayoutedReader.Tests.Layouts
{
    public class DeployerTests
    {
        [Test]
        public void can_deploy_test_a_file_without_deploy()
        {
            var layout = SimpleSerializer.Xml<Layout>()
                .DeserializeTypedFromString(SampleLayouts.test_a_layout);
            var deployer = new Deployer(layout);

            var items = deployer.Read(SampleLayouts.test_a.ToStream()).ToList();
            items.Sum(x => x.Expanded.Count).Should().Be(0);
        }

        [Test]
        public void can_deploy_test_a_file_with_deploy()
        {
            var layout = SimpleSerializer.Xml<Layout>()
                .DeserializeTypedFromString(SampleLayouts.test_a_layout);
            var deploy = SimpleSerializer.Xml<Filter>()
                .DeserializeTypedFromString(SampleLayouts.test_a_deploy);

            var deployer = new Deployer(layout, deploy);

            var items = deployer.Read(SampleLayouts.test_a.ToStream()).ToList();
            items.Sum(x => x.Expanded.Count).Should().Be(76);
        }
    }
}
