using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Serialization;
using LayoutedReader.Tests.Layouts.Examples;
using LayoutedReader.Layouts;
using LayoutedReader;
using SharpTestsEx;

namespace LayoutedReader.Tests.Layouts
{
    [TestFixture]
    public class SampleFilterFixture
    {
        Filter filter;

        [TestFixtureSetUp]
        public void Setup()
        {
            filter = SimpleSerializer.Xml<Filter>()
                .DeserializeTypedFromString(SampleLayouts.SampleFilters);
        }

        [Test]
        public void when_doesnt_match_returns_empty()
        {
            var ctx = new RecordContext(null, ValueBag.Create(prop => 4));
            filter.Evaluate(ctx).Should().Be.Empty();
        }

        [Test]
        public void header_fields_also_enter_in_evaluated_bag()
        {
            var ctx = new RecordContext(ValueBag.Create(head => 2), ValueBag.Create(prop => 3));
            var bags = filter.Evaluate(ctx).ToList();
            bags.Count.Should().Be(1);
            bags[0]["head"].Value.Should().Be(2);
        }

        [Test]
        public void when_match_only_action_filter_returns_1()
        {
            var ctx = new RecordContext(null, ValueBag.Create(prop => 3));
            var bags = filter.Evaluate(ctx).ToList();
            bags.Count.Should().Be(1);
            Assert(bags[0], 3, "K", "M");
        }

        [Test]
        public void when_match_ordinary_filter_returns_4()
        {
            var ctx = new RecordContext(null, ValueBag.Create(prop => 2, op => "A"));
            var bags = filter.Evaluate(ctx).ToList();
            bags.Count.Should().Be(4);

            Assert(bags[0], 2, "R", "C");
            Assert(bags[1], 2, "T", "V");
            Assert(bags[2], 2, "T", "C");

            Assert(bags[3], 2, "K", "M");
        }

        [Test]
        public void when_match_two_ordinary_filter_returns_6()
        {
            var ctx = new RecordContext(null, ValueBag.Create(prop => 2, op => "B", dc => "Q"));
            var bags = filter.Evaluate(ctx).ToList();
            bags.Count.Should().Be(6);

            Assert(bags[0], 2, "R", "C");
            Assert(bags[1], 2, "T", "V");
            Assert(bags[2], 2, "T", "C");

            Assert(bags[3], 2, "T", "V");
            Assert(bags[4], 2, "T", "C");

            Assert(bags[5], 2, "K", "M");
        }

        [Test]
        public void when_match_special_filter_ignore_others()
        {
            var ctx = new RecordContext(null, ValueBag.Create(prop => 2, op => "B", dc => "Q", atv => "ASD"));
            var bags = filter.Evaluate(ctx).ToList();
            bags.Count.Should().Be(2);

            Assert(bags[0], 2, "R", "C");
            Assert(bags[1], 2, "K", "M");

        }

        private void Assert(ValueBag bag, int prop, string tipo, string parte)
        {
            bag["prop"].Should().Be(new ValueItem(prop));
            bag["tipoOperacao"].Should().Be(new ValueItem(tipo));
            bag["parteCobrada"].Should().Be(new ValueItem(parte));
        }

    }
}
