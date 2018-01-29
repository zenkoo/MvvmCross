// MvxColorValueConverterTest.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using MvvmCross.Platform.UI;
using MvvmCross.Test;
using Xunit;

namespace MvvmCross.Plugins.Color.Test
{
    public class MvxColorValueConverterTest : IClassFixture<MvxTestFixture>
    {
        public MvxTestFixture Fixture { get; }

        public MvxColorValueConverterTest(MvxTestFixture fixture)
        {
            Fixture = fixture;
            Fixture.Ioc.RegisterSingleton<IMvxNativeColor>(new MockNative());
        }

        public class WrappedColor
        {
            public WrappedColor(MvxColor color)
            {
                Color = color;
            }

            public MvxColor Color { get; private set; }
        }

        public class MockNative : IMvxNativeColor
        {
            public object ToNative(MvxColor mvxColor)
            {
                return new WrappedColor(mvxColor);
            }
        }
    }

    [CollectionDefinition("Color")]
    public class DatabaseCollection : ICollectionFixture<MvxTestFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
