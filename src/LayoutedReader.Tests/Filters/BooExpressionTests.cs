using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LayoutedReader;
using Simple.Reflection;
using System.Collections.Specialized;
using LayoutedReader.Filters;
using SharpTestsEx;

namespace LayoutedReader.Tests.Filters
{
    [TestFixture]
    public class BooExpressionTests
    {
        [Test]
        public void can_evaluate_more_than_one_bag()
        {
            var ctx1 = ValueBag.Create(a => 1);
            var ctx2 = ValueBag.Create(b => "2");

            var expr1 = new BooExpression("c.a == 1 and c.b == '2'");
            expr1.AppliesTo(ctx1, ctx2).Should().Be.True();
        }

        [Test]
        public void can_find_value_in_context()
        {
            var ctx = ValueBag.Create(a => 1, b => "2");
            var expr1 = new BooExpression("c.a == 1 and c.b == '2'");
            expr1.AppliesTo(ctx).Should().Be.True();
        }

        [Test]
        public void wrong_type_causes_invalidation()
        {
            var ctx = ValueBag.Create(a => 1, b => "2");
            var expr1 = new BooExpression("c.a == 1 and c.b == 2");
            expr1.AppliesTo(ctx).Should().Be.False();
        }

        [Test]
        public void list_type_accepts_operator_in_when_exists_item()
        {
            var ctx = ValueBag.Create(a => 2);
            var expr1 = new BooExpression("c.a in (1,2,3)");
            expr1.AppliesTo(ctx).Should().Be.True();
        }

        [Test]
        public void list_type_accepts_operator_in_when_not_exists_item()
        {
            var ctx = ValueBag.Create(a => 4);
            var expr1 = new BooExpression("c.a in (1,2,3)");
            expr1.AppliesTo(ctx).Should().Be.False();
        }

        [Test]
        public void list_type_accepts_operator_not_in_when_exists_item()
        {
            var ctx = ValueBag.Create(a => 2);
            var expr1 = new BooExpression("c.a not in (1,2,3)");
            expr1.AppliesTo(ctx).Should().Be.False();
        }

        [Test]
        public void list_type_accepts_operator_not_iln_when_not_exists_item()
        {
            var ctx = ValueBag.Create(a => 4);
            var expr1 = new BooExpression("c.a not in (1,2,3)");
            expr1.AppliesTo(ctx).Should().Be.True();
        }

        [Test]
        public void special_chars_in_key_are_removed()
        {
            var ctx = new ValueBag { { "1a-b", new ValueItem(3) } };
            var expr1 = new BooExpression("c._a_b == 3");
            expr1.AppliesTo(ctx).Should().Be.True();
        }


    }
}
