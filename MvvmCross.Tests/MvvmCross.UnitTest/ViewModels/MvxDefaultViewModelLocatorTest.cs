﻿// MvxDefaultViewModelLocatorTest.cs

// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using MvvmCross.Core.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Exceptions;
using MvvmCross.Test.Core;
using MvvmCross.Test.Mocks.TestViewModels;
using Xunit;

namespace MvvmCross.Test.ViewModels
{
    
    public class MvxDefaultViewModelLocatorTest : MvxIoCSupportingTest
    {
        [Fact]
        public void Test_NoReloadState()
        {
            ClearAll();

            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());

            var testThing = new MockTestThing();
            Ioc.RegisterSingleton<ITestThing>(testThing);

            var testObject = new BundleObject
            {
                TheBool1 = false,
                TheBool2 = true,
                TheGuid1 = Guid.NewGuid(),
                TheGuid2 = new Guid(123, 10, 444, 1, 2, 3, 4, 5, 6, 7, 8),
                TheInt1 = 123,
                TheInt2 = 456,
                TheString1 = "Hello World",
                TheString2 = null
            };
            var bundle = new MvxBundle();
            bundle.Write(testObject);

            var toTest = new MvxDefaultViewModelLocator();

            IMvxViewModel viewModel = toTest.Load(typeof(Test1ViewModel), bundle, null);

            Assert.IsNotNull(viewModel);
            var typedViewModel = (Test1ViewModel)viewModel;
            Assert.AreSame(bundle, typedViewModel.BundleInit);
            Assert.IsNull(typedViewModel.BundleState);
            Assert.AreSame(testThing, typedViewModel.Thing);
            Assert.Equal(testObject, typedViewModel.TheInitBundleSet);
            Assert.IsNull(typedViewModel.TheReloadBundleSet);
            Assert.Equal(testObject.TheGuid1, typedViewModel.TheInitGuid1Set);
            Assert.Equal(testObject.TheGuid2, typedViewModel.TheInitGuid2Set);
            Assert.Equal(testObject.TheString1, typedViewModel.TheInitString1Set);
            Assert.Equal(Guid.Empty, typedViewModel.TheReloadGuid1Set);
            Assert.Equal(Guid.Empty, typedViewModel.TheReloadGuid2Set);
            Assert.Equal(null, typedViewModel.TheReloadString1Set);
            Assert.IsTrue(typedViewModel.StartCalled);
        }

        [Fact]
        public void Test_WithReloadState()
        {
            ClearAll();

            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());

            var testThing = new MockTestThing();
            Ioc.RegisterSingleton<ITestThing>(testThing);

            var initBundleObject = new BundleObject
            {
                TheBool1 = false,
                TheBool2 = true,
                TheGuid1 = Guid.NewGuid(),
                TheGuid2 = new Guid(123, 10, 444, 1, 2, 3, 4, 5, 6, 7, 8),
                TheInt1 = 123,
                TheInt2 = 456,
                TheString1 = "Hello World",
                TheString2 = null
            };
            var initBundle = new MvxBundle();
            initBundle.Write(initBundleObject);

            var reloadBundleObject = new BundleObject
            {
                TheBool1 = true,
                TheBool2 = true,
                TheGuid1 = Guid.NewGuid(),
                TheGuid2 = new Guid(1123, 10, 444, 1, 2, 3, 4, 5, 6, 7, 8),
                TheInt1 = 1234,
                TheInt2 = 4567,
                TheString1 = "Foo Bar",
                TheString2 = null
            };
            var reloadBundle = new MvxBundle();
            reloadBundle.Write(reloadBundleObject);

            var toTest = new MvxDefaultViewModelLocator();
            IMvxViewModel viewModel = toTest.Load(typeof(Test1ViewModel), initBundle, reloadBundle);

            Assert.IsNotNull(viewModel);
            var typedViewModel = (Test1ViewModel)viewModel;
            Assert.AreSame(initBundle, typedViewModel.BundleInit);
            Assert.AreSame(reloadBundle, typedViewModel.BundleState);
            Assert.AreSame(testThing, typedViewModel.Thing);
            Assert.Equal(initBundleObject, typedViewModel.TheInitBundleSet);
            Assert.Equal(reloadBundleObject, typedViewModel.TheReloadBundleSet);
            Assert.Equal(initBundleObject.TheGuid1, typedViewModel.TheInitGuid1Set);
            Assert.Equal(initBundleObject.TheGuid2, typedViewModel.TheInitGuid2Set);
            Assert.Equal(initBundleObject.TheString1, typedViewModel.TheInitString1Set);
            Assert.Equal(reloadBundleObject.TheGuid1, typedViewModel.TheReloadGuid1Set);
            Assert.Equal(reloadBundleObject.TheGuid2, typedViewModel.TheReloadGuid2Set);
            Assert.Equal(reloadBundleObject.TheString1, typedViewModel.TheReloadString1Set);
            Assert.IsTrue(typedViewModel.StartCalled);
        }

        [Fact]
        public void Test_MissingDependency()
        {
            ClearAll();

            var bundle = new MvxBundle();

            var toTest = new MvxDefaultViewModelLocator();

            Assert.That(
                () => {
                    IMvxViewModel viewModel = toTest.Load(typeof(Test4ViewModel), bundle, null);
                },
                Throws.TypeOf<MvxException>().With.Message.StartWith("Problem creating viewModel"));
        }

        [Fact]
        public void Test_FailingDependency()
        {
            ClearAll();

            Ioc.RegisterSingleton<ITestThing>(() => new FailingMockTestThing());

            var bundle = new MvxBundle();

            var toTest = new MvxDefaultViewModelLocator();

            Assert.That(
                () => {
                    IMvxViewModel viewModel = toTest.Load(typeof(Test4ViewModel), bundle, null);
                },
                Throws.TypeOf<MvxException>().With.Message.StartWith("Problem creating viewModel"));
        }

        [Fact]
        public void Test_FailingInitialisation()
        {
            ClearAll();

            var testThing = new MockTestThing();
            Ioc.RegisterSingleton<ITestThing>(testThing);

            var bundle = new MvxBundle();

            var toTest = new MvxDefaultViewModelLocator();

            Assert.That(
                () => {
                    IMvxViewModel viewModel = toTest.Load(typeof(Test4ViewModel), bundle, null);
                },
                Throws.TypeOf<MvxException>().With.Message.StartWith("Problem running viewModel lifecycle"));
        }
    }
}