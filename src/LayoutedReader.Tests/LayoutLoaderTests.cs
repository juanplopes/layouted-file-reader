using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cetip.FileReader;
using NUnit.Framework;
using System.IO.Abstractions.TestingHelpers;
using Cetip.Tests.Layouts;
using SharpTestsEx;
using Simple.Patterns;

namespace Cetip.Tests
{
    [TestFixture]
    public class LayoutLoaderTests
    {
        LayoutLoader loader;

        class Test : BaseWorkingDaysProvider
        {
            public override IEnumerable<DayOfWeek> FixedNonWorkingDays()
            {
                throw new NotImplementedException();
            }

            public override int GetNetDynamicNonWorkingDays(DateTime date1, DateTime date2)
            {
                throw new NotImplementedException();
            }

            public override bool IsDynamicNonWorkingDay(DateTime date)
            {
                throw new NotImplementedException();
            }
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            var system = new MockFileSystem(new Dictionary<string, MockFileData> {
                { @"e:\a\b\c\layout.xml",new MockFileData(SampleLayouts.Layout)},
                { @"e:\a\b\c\layouts\c21.xml",new MockFileData(SampleLayouts.C21)},
                { @"e:\a\b\c\layouts\c21custodia.xml",new MockFileData(SampleLayouts.C21Custodia)}
            });

            loader = new LayoutLoader(system, @"e:\a\b\c\layout.xml");
        }

        [Test]
        public void must_have_two_layouts_registered()
        {
            loader.Mappings.Mappings.Count().Should().Be(2);
        }
    }
}
