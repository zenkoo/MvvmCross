// MvxViewModelViewLookupBuilderTest.cs

// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using MvvmCross.Core.ViewModels;
using MvvmCross.Test.Core;
using MvvmCross.Test.Mocks.TestViewModels;
using MvvmCross.Test.Mocks.TestViews;
using Xunit;

namespace MvvmCross.Test.Platform
{
    
    public class MvxViewModelViewLookupBuilderTest : MvxIoCSupportingTest
    {
        [Fact]
        public void Test_Builder()
        {
            ClearAll();

            var assembly = GetType().Assembly;
            var viewModelNameLookup = new MvxViewModelByNameLookup();
            viewModelNameLookup.AddAll(assembly);
            var nameMapping = new MvxPostfixAwareViewToViewModelNameMapping("View", "Oddness");
            var finder = new MvxViewModelViewTypeFinder(viewModelNameLookup, nameMapping);
            Ioc.RegisterSingleton<IMvxViewModelTypeFinder>(finder);

            var builder = new MvxViewModelViewLookupBuilder();
            var result = builder.Build(new[] { assembly });

            Assert.Equal(4, result.Count);
            Assert.Equal(typeof(Test1View), result[typeof(Test1ViewModel)]);
            Assert.Equal(typeof(NotTest2View), result[typeof(Test2ViewModel)]);
            Assert.Equal(typeof(NotTest3View), result[typeof(Test3ViewModel)]);
            Assert.Equal(typeof(OddNameOddness), result[typeof(OddNameViewModel)]);
        }
    }
}