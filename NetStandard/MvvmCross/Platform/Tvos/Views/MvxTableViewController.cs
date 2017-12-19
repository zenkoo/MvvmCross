﻿// MvxTableViewController.cs

// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.tvOS.Views;
using UIKit;

namespace MvvmCross.tvOS.Views
{
    public class MvxTableViewController
        : MvxEventSourceTableViewController, IMvxTvosView
    {
        protected MvxTableViewController(UITableViewStyle style = UITableViewStyle.Plain)
            : base(style)
        {
            this.AdaptForBinding();
        }

        protected MvxTableViewController(IntPtr handle)
            : base(handle)
        {
            this.AdaptForBinding();
        }

        protected MvxTableViewController(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
            this.AdaptForBinding();
        }

        public object DataContext
        {
            get { return BindingContext.DataContext; }
            set { BindingContext.DataContext = value; }
        }

        public IMvxViewModel ViewModel
        {
            get
            {
                /*
				MvxLog.Instance.Trace ("I am in .ViewModel!");
				if (BindingContext == null)
					MvxLog.Instance.Trace ("BindingContext is null!");
				MvxLog.Instance.Trace ("I am in .ViewModel 2!");
				if (DataContext == null)
					MvxLog.Instance.Trace ("DataContext is null!");
				MvxLog.Instance.Trace ("I am in .ViewModel 3!");

				var c = DataContext;
				MvxLog.Instance.Trace ("I am in .ViewModel 4!");
				var d = c as IMvxViewModel;
				MvxLog.Instance.Trace ("I am in .ViewModel 5!");

				var e = (IMvxViewModel)d;
				MvxLog.Instance.Trace ("I am in .ViewModel 6!");
				if (d == null)
					MvxLog.Instance.Trace ("d was null!");

				if (e == null)
					MvxLog.Instance.Trace ("e was null!");
				*/
                return DataContext as IMvxViewModel;
            }
            set { DataContext = value; }
        }

        public MvxViewModelRequest Request { get; set; }

        public IMvxBindingContext BindingContext { get; set; }
    }

    public class MvxTableViewController<TViewModel>
        : MvxTableViewController, IMvxTvosView<TViewModel> where TViewModel : class, IMvxViewModel
    {
        protected MvxTableViewController(UITableViewStyle style = UITableViewStyle.Plain)
            : base(style)
        {
        }

        protected MvxTableViewController(IntPtr handle)
            : base(handle)
        {
        }

        protected MvxTableViewController(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
        }

        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}