﻿// MvxIoCSupportingTest.cs

// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Globalization;
using MvvmCross.Core;
using MvvmCross.Core.Platform;
using MvvmCross.Platform.Core;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform.Logging;
using MvvmCross.Platform.Logging.LogProviders;

namespace MvvmCross.Test
{
    public class MvxIoCSupportingTest
    {
        public IMvxIoCProvider Ioc { get; private set; }

        public void Setup()
        {
            ClearAll();
        }

        public void Reset()
        {
            MvxSingleton.ClearAllSingletons();
        }

        protected virtual IMvxIocOptions CreateIocOptions()
        {
            return null;
        }

        public virtual void ClearAll()
        {
            // fake set up of the IoC
            Reset();
            Ioc = MvxIoCProvider.Initialize(CreateIocOptions());
            Ioc.RegisterSingleton(Ioc);
            InitializeSingletonCache();
            InitializeMvxSettings();
            AdditionalSetup();
            Ioc.RegisterSingleton<IMvxLogProvider>(new ConsoleLogProvider());
        }

        public void InitializeSingletonCache()
        {
            MvxSingletonCache.Initialize();
        }

        protected virtual void InitializeMvxSettings()
        {
            Ioc.RegisterSingleton<IMvxSettings>(new MvxSettings());
        }

        protected virtual void AdditionalSetup()
        {
            // nothing here..
        }

        public void SetInvariantCulture()
        {
            var invariantCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = invariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = invariantCulture;
        }
    }
}
