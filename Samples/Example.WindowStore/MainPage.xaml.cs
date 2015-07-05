﻿using Cirrious.CrossCore;
using Cirrious.MvvmCross.Forms.Presenter.WindowsStore;
using Cirrious.MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Example.WindowStore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : global::Xamarin.Forms.Platform.WinRT.WindowsPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsWindowsStorePagePresenter;
            LoadApplication(presenter.MvxFormsApp);
        }
    }
}
