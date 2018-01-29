// MvxViewModelViewTypeFinderTest.cs

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
    
    public class MvxViewModelViewTypeFinderTest : MvxIoCSupportingTest
    {
        [Fact]
        public void Test_MvxViewModelViewTypeFinder()
        {
            ClearAll();

            var assembly = GetType().Assembly;
            var viewModelNameLookup = new MvxViewModelByNameLookup();
            viewModelNameLookup.AddAll(assembly);
            var nameMapping = new MvxPostfixAwareViewToViewModelNameMapping("View", "Oddness");
            var test = new MvxViewModelViewTypeFinder(viewModelNameLookup, nameMapping);

            // test for positives
            var result = test.FindTypeOrNull(typeof(Test1View));
            Assert.Equal(typeof(Test1ViewModel), result);
            result = test.FindTypeOrNull(typeof(NotTest2View));
            Assert.Equal(typeof(Test2ViewModel), result);
            result = test.FindTypeOrNull(typeof(NotTest3View));
            Assert.Equal(typeof(Test3ViewModel), result);
            result = test.FindTypeOrNull(typeof(OddNameOddness));
            Assert.Equal(typeof(OddNameViewModel), result);

            // test for negatives
            result = test.FindTypeOrNull(typeof(AbstractTest1View));
            Assert.IsNull(result);
            result = test.FindTypeOrNull(typeof(NotReallyAView));
            Assert.IsNull(result);
        }
    }
}