#region

using System;
using System.Collections.Generic;
using Xunit;
using Shouldly;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class EnumerableExtensionTests
    {
        [Fact]
        public void ForEachItem_ThrowsException()
        {
            var i = 0;
            Assert.Throws<NullReferenceException>(() => ((List<object>)null).ForEachItem(c => i++));
        }

        [Fact]
        public void ForEachItem_IteratesOnCollection()
        {
            var i = 0;
            new[] { 1, 2, 3 }.ForEachItem(c => i++);
            i.ShouldBe(3);
        }

        [Fact]
        public void IsEmptyOrNull_returnsTrueCases()
        {
            new List<string>().IsEmptyOrNull().ShouldBeTrue();

            ((IEnumerable<string>)null).IsEmptyOrNull().ShouldBeTrue();
            "".IsEmptyOrNull().ShouldBeTrue();
        }

        [Fact]
        public void IsEmptyOrNull_ReturnsFalseCases()
        {
            "Test".IsEmptyOrNull().ShouldBeFalse();
        }
        [Fact]
        public void Random_PicksItems()
        {
            var action = new Action(() =>
            {
                var source = new[] { 1, 2 };
                var actualValue = source.Random();
                (1 == actualValue || 2 == actualValue).ShouldBeTrue();

            });

            for (int i = 0; i < 1000; i++)
            {
                action();
            }
        }
    }
}