using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LayoutedReader;
using SharpTestsEx;

namespace LayoutedReader.Tests
{
    [TestFixture]
    public class ValueBagTests
    {
        [Test]
        public void passing_correct_case_key_will_cast_correct_struct()
        {
            var dic = new ValueBag { { "asd", new ValueItem(123) } };
            dic.GetAs<int>("asd").Should().Be(123);
            dic["asd"].Value.Should().Be(123);
        }

        [Test]
        public void getting_values_of_several_keys_work_if_all_keys_are_present()
        {
            var dic = ValueBag.Create(asd => 123, qwe => 234);
            dic.ValuesOf("asd", "qwe").Should().Have.SameSequenceAs(123, 234);
        }

        [Test]
        public void getting_values_of_several_keys_work_even_if_not_all_keys_are_present()
        {
            var dic = ValueBag.Create(asd => 123, qwe => 234);
            dic.ValuesOf("asd", "qwe", "wer")
                .Should().Have.SameSequenceAs(123, 234, null);
        }

        [Test]
        public void passing_correct_case_key_will_cast_correct_class()
        {
            var dic = new ValueBag { { "asd", new ValueItem("qwe") } };
            dic.GetAs<string>("asd").Should().Be("qwe");
            dic["asd"].Value.Should().Be("qwe");
        }

        [Test]
        public void passing_different_case_key_will_cast_correct_struct()
        {
            var dic = new ValueBag { { "asd", new ValueItem(123) } };
            dic.GetAs<int>("ASD").Should().Be(123);
            dic["ASD"].Value.Should().Be(123);
        }

        [Test]
        public void passing_different_case_key_will_cast_correct_class()
        {
            var dic = new ValueBag { { "asd", new ValueItem("qwe") } };
            dic.GetAs<string>("ASD").Should().Be("qwe");
            dic["ASD"].Value.Should().Be("qwe");
        }

        [Test]
        public void passing_non_existing_key_will_cast_correct_struct()
        {
            var dic = new ValueBag { { "asd", new ValueItem(123) } };
            dic.GetAs<int>("BBB").Should().Be(0);
            dic.GetAs<int?>("BBB").Should().Be(null);
            dic.Executing(x => x["BBB"].ToString()).Throws<KeyNotFoundException>();
        }

        [Test]
        public void passing_non_existing_key_will_cast_correct_class()
        {
            var dic = new ValueBag { { "asd", new ValueItem("qwe") } };
            dic.GetAs<string>("BBB").Should().Be(null);
            dic.Executing(x => x["BBB"].ToString()).Throws<KeyNotFoundException>(); ;
        }

        [Test]
        public void custom_formatter_works_for_key_retrieval()
        {
            var dic = new ValueBag { { "_1o-n-e", new ValueItem("qwe") } };
            dic.GetAs<string>("_1o-n-e").Should().Be("qwe");
            dic["_1o_n_e"].Value.Should().Be("qwe");
        }

        [Test]
        public void identifiers_starting_with_number_are_corrected()
        {
            ValueBag.KeyComparer.IdentifierFormat("1one").Should().Be("_one");
        }

        [Test]
        public void identifiers_starting_with_underscore_arent_corrected()
        {
            ValueBag.KeyComparer.IdentifierFormat("_one").Should().Be("_one");
        }

        [Test]
        public void non_identifiers_are_removed()
        {
            ValueBag.KeyComparer.IdentifierFormat("_1o-n-e").Should().Be("_1o_n_e");
        }
    }
}
