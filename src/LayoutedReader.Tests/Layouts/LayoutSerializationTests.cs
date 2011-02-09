using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.IO.Serialization;
using LayoutedReader;
using SharpTestsEx;
using LayoutedReader.Layouts;

namespace LayoutedReader.Tests.Layouts
{
    [TestFixture]
    public class LayoutSerializationTests
    {
        [Test]
        public void can_deserialize_empty_layout()
        {
            var str = @"<layout></layout>";
            var obj = SimpleSerializer.Xml<Layout>().DeserializeTypedFromString(str);
            obj.Fields.Should().Be.Empty();
        }

        [Test]
        public void can_deserialize_one_field_layout()
        {
            var str = @"<layout><field name='test1' type='number(5)'/></layout>";
            var obj = SimpleSerializer.Xml<Layout>().DeserializeTypedFromString(str);
            obj.Fields.Should().Have.SameSequenceAs(
                new Field("test1", "number(5)", null));
        }

        [Test]
        public void can_deserialize_two_fields_layout()
        {
            var str =
@"<layout>
    <field name='test1' type='number(5)'/>
    <field name='test2' type='date' format='string(F3,pt-BR)'/>
</layout>";
            var obj = SimpleSerializer.Xml<Layout>().DeserializeTypedFromString(str);
            obj.Fields.Should().Have.SameSequenceAs(
                new Field("test1", "number(5)", null),
                new Field("test2", "date", "string(F3,pt-BR)"));
        }
    }
}
