// MvxPropertyNameFromExpressionTests.cs

// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using MvvmCross.Platform.Core;
using Xunit;

namespace MvvmCross.Platform.Test
{
    
    public class MvxPropertyNameFromExpressionTests
    {
        public class TestClass
        {
            public string Foo { get; set; }

            public string GetFooExpression()
            {
                return this.GetPropertyNameFromExpression(() => Foo);
            }
        }

        [Fact]
        public void TestPropertyExpression()
        {
            var t = new TestClass();
            var result = t.GetFooExpression();
            Assert.Equal("Foo", result);
        }

        [Fact]
        public void TestUnaryPropertyExpression()
        {
            var t = new TestClass();
            var result = t.GetPropertyNameFromExpression(() => t.Foo);
            Assert.Equal("Foo", result);
        }
    }
}